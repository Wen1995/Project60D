using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using UnityEngine;
using com.game.framework.protocol;

public class RPCNetwork{
    //--temp-----------------------------------------
    const int MAX_READ = 1024 * 14;
    //--reference------------------------------------
    private TcpClient mTcpClient = null;
    private NetworkStream mOutStream = null;
    byte[] mByteBuffer = new byte[MAX_READ];        //Socket OutStream Buffer
    private NMemoryStream mSendStream = new NMemoryStream(1024 * 16);
    private NMemoryStream mRecieveStream = new NMemoryStream(1024 * 16);
    private Queue<NetMsgDef> mSendMsgQueue = new Queue<NetMsgDef>();
    private Queue<NetMsgDef> mRecieveMsgQueue = new Queue<NetMsgDef>();
    List<Error> mErrorList = new List<Error>();
    //--attribute------------------------------------
    private NetType mNetType;
    private bool isConnected = false;               //Set true once connected to server
    //--callback-------------------------------------
    private RPCResponceCallback mResponceCallback;
    private RPCExceptionCallback mExceptionCallback;



    public RPCNetwork(NetType netType, RPCResponceCallback responceCallback, RPCExceptionCallback exceptionCallback)
    {
        mNetType = netType;
        mResponceCallback = responceCallback;
        mExceptionCallback = exceptionCallback;
    }

    /// <summary>
    /// Called by manager per frame
    /// </summary>
    public void Update()
    {
        if (!isConnected) return;
        //TODO
        //check connection

        //send msg
        SendNetMsg();

        //handle recieved msg
        HandleNetMsg();

        //handle exception
        HandleException();
    }

    //Begin connecting to server
    public void DoConnect(string host, int port)
    {
        mTcpClient = new TcpClient();
        mTcpClient.BeginConnect(IPAddress.Parse(host), port, new AsyncCallback(OnConnected), null);
    }

    public void CutOff()
    {
        mTcpClient.GetStream().Close();
        mTcpClient.Close();
    }

    void OnConnected(IAsyncResult ar)
    {
        Debug.Log("RPCNetwork Connected");
        mTcpClient.EndConnect(ar);
        mOutStream = mTcpClient.GetStream();
        mOutStream.BeginRead(mByteBuffer, 0, MAX_READ, OnRead, null);

        isConnected = true;
    }

    //Clear Data And Buffer
    void Disconnect()
    {
        //TODO
        //Clear Data
        if (mOutStream != null)
        {
            mOutStream.Dispose();
            mOutStream.Close();
        }
        Array.Clear(mByteBuffer, 0, MAX_READ);
        mSendStream.Reset();
        mRecieveStream.Reset();
    }

    /// <summary>
    /// Callback Of Writing To NetSteam, Asynchronous
    /// </summary>
    void OnWrite(IAsyncResult ar)
    {
        try
        {
            mOutStream.EndWrite(ar);
        }
        catch (Exception e)
        {
            //OnException(e);
            UnityEngine.Debug.Log(e);
            Disconnect();
        }
    }

    //Callback For Reading Data From Server
    void OnRead(IAsyncResult ar)
    {
        try
        {
            int byteSize = 0;
            byteSize = mOutStream.EndRead(ar);
            if (byteSize < 1)   //might be losing connection
            {
                Disconnect();
            }
            else
            {
                RecieveNetMsg(mByteBuffer, byteSize);
            }
        }
        catch (Exception e)
        {
            //OnException(e);
            UnityEngine.Debug.Log(e);
            Disconnect();
        }
        finally
        {
            Array.Clear(mByteBuffer, 0, MAX_READ);
            mOutStream.BeginRead(mByteBuffer, 0, MAX_READ, OnRead, null);
        }
    }

    /// <summary>
    /// Accept a batch of NetMsg & store to the queue
    /// </summary>
    public void SubmitNetMsg(List<NetMsgDef> msgList)
    {
        if (msgList.Count < 1)
            return;
        lock (mSendMsgQueue)
        {
            foreach (var msg in msgList)
                mSendMsgQueue.Enqueue(msg);
        }
    }

    /// <summary>
    /// Try wrtiting to scoket from NetMsg queue
    /// </summary>
    void SendNetMsg()
    {
        if (mSendMsgQueue.Count < 1) return;
        mSendStream.Reset();
        lock (mSendMsgQueue)
        {
            while (mSendMsgQueue.Count >= 1)
            {
                NetMsgDef msg = mSendMsgQueue.Dequeue();
                if (msg == null) return;
                if (!mSendStream.Write(msg))
                    break;
            }
        }
        if(mSendStream.WritePos > 0)
            mOutStream.BeginWrite(mSendStream.Data, 0, (int)mSendStream.WritePos, OnWrite, null);
    }

    /// <summary>
    /// Recieve Netmsg from socket & store to buffer
    /// </summary>
    void RecieveNetMsg(byte[] data, int size)
    {
        if (!mRecieveStream.Write(data, size))
        {
            Disconnect();
            return;
        }
            
        while (true)
        {
            NetMsgDef msg = null;
            NetMsgHead head = null;
            
            if (!mRecieveStream.Read(ref head))                         //Cant read msg head
                break;
            if (mRecieveStream.RemainReadCap < (uint)head.mSize - sizeof(short))        //Package is not completed, wait to next frame
            {
                //Restore ReadPos
                mRecieveStream.ShiftReadPos(-4);
                break; 
            }
            byte[] btsData = new byte[head.mSize - sizeof(short)];
            if (!mRecieveStream.Read(btsData, head.mSize - sizeof(short)))
                break;
            msg = new NetMsgDef(head, btsData);
            lock (mRecieveMsgQueue)
            {
                mRecieveMsgQueue.Enqueue(msg);
            }
        }
    }

    /// <summary>
    /// Throw NetMsg back to higer level, called per frame
    /// </summary>
    void HandleNetMsg()
    {
        if (mRecieveMsgQueue.Count < 1) return;
        int count = 20;      //handle 20 packages most per frame
        lock (mRecieveMsgQueue)
        {
            while (count-- > 0 && mRecieveMsgQueue.Count > 0)
            {
                NetMsgDef msg = mRecieveMsgQueue.Dequeue();
                if (msg == null) continue;
                mResponceCallback(mNetType, msg);
            }
        }
    }

    /// <summary>
    /// Add a exception to list
    /// </summary>
    void OnException(Error e)
    {
        Debug.Log(string.Format("Caught error {0}", e.ToString()));
        //bandon adding duplicate exception
        if(!mErrorList.Contains(e))
            mErrorList.Add(e);
    }


    void HandleException()
    {
        if (mErrorList.Count < 1)
            return;
        lock (mErrorList)
        {
            for (int i = 0; i < mErrorList.Count; i++)
            {
                //mExceptionCallback()
                mExceptionCallback(mNetType, mErrorList[i]);
            }
            mErrorList.Clear();
        }
    }
}
