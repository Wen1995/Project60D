using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Google.ProtocolBuffers;

public class TCPServer : MonoBehaviour {

    //--attribute
    const string HOST = "127.0.0.1";
    const int PORT = 8000;
    const int MAX_READ = 1024 * 14;
    const string msg = "fine thank u";
    //--reference
    byte[] mByteBuffer = new byte[MAX_READ];
    TcpListener mTcpListener;
    TcpClient mRemoteClient;
    NetworkStream mOutStream;
    MemoryStream recvStream = new MemoryStream(1024 * 16);
    MemoryStream sendStream = new MemoryStream(1024 * 16);
    // Use this for initialization
    void Start() {
        mTcpListener = new TcpListener(IPAddress.Parse(HOST), PORT);
        mTcpListener.Start();
        mTcpListener.BeginAcceptTcpClient(OnConnected, mTcpListener);
    }

    void OnConnected(IAsyncResult ar)
    {
        try
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            mRemoteClient = listener.EndAcceptTcpClient(ar);
            mOutStream = mRemoteClient.GetStream();
            mOutStream.BeginRead(mByteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
            print("server get client");
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }

    void OnRead(IAsyncResult ar)
    {
        try
        {
            int byteSize = 0;
            byteSize = mOutStream.EndRead(ar);
            if (byteSize < 1)
            {
            }
            else
            {
                print("server get msg!!");
                recvStream.Write(mByteBuffer, byteSize);
                //NetMsgDef msg = null;
                //recvStream.Read(ref msg);
                //ByteString btsString = ByteString.CopyFrom(msg.mBtsData);
                //CSLogin login = CSLogin.ParseFrom(btsString);
                //print("UserName: " + login.UserName);
                //print("Password: " + login.Password);
                //SendMsg();
            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            Array.Clear(mByteBuffer, 0, MAX_READ);
            mOutStream.BeginRead(mByteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
            recvStream.Reset();
        }
    }

    void OnRecieveMsg(byte[] buffer, int size)
    {
        if (!recvStream.Write(buffer, size))
            return;
        while (true)
        {

        }
    }


    void SendMsg()
    {
        //var builder = CSLogin.CreateBuilder();
        //builder.UserName = "Server";
        //builder.Password = "123";
        //CSLogin login = builder.Build();
        //byte[] data = login.ToByteArray();
        //NetMsgDef msg = RPCNetTools.CreateRpcMsg((short)Cmd.LOGIN, data);
        //sendStream.Reset();
        //sendStream.Write(msg);
        //mOutStream.BeginWrite(sendStream.Data, 0, (int)sendStream.WritePos, OnWrite, null);
    }

    void OnWrite(IAsyncResult r)
    {
        mOutStream.EndWrite(r);
    }
}
