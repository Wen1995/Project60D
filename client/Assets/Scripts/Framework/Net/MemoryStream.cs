using System.Collections;
using System.Collections.Generic;
using System;

public class MemoryStream {

    //attribute
    byte[] mByteBuffer = null;
    uint mCapacity;
    uint mWritePos;
    uint mReadPos;

    bool isBigEndian = true;                    //need reverse bytes?

    //data acess
    public byte[] Data { get { return mByteBuffer; } }
    public uint RemainWriteCap { get { return mCapacity >= mWritePos ? (mCapacity - mWritePos) : 0; } }
    public uint RemainReadCap { get { return WritePos > ReadPos ? (WritePos - ReadPos) : 0; } }
    public uint WritePos { get { return mWritePos; } }
    public uint ReadPos { get { return mReadPos; } }


    public MemoryStream(int capacity)
    {
        mByteBuffer = new byte[capacity];
        mCapacity = (uint)capacity;
        mWritePos = 0;
        mReadPos = 0;
    }

    //Clear Buffer
    public void Reset()
    {
        mWritePos = 0;
        mReadPos = 0;
    }

    public bool CheckWriteCap(int size)
    {
        if (RemainWriteCap < size)
            return HandleWriteSpace(size);
        return true;
    }

    public bool CheckReadCap(int size)
    {
        if (RemainReadCap < size)
            return false;
        return true;
    }

    //Move data between ReadPos & WritePos to buffer head
    public bool HandleWriteSpace(int size)
    {
        try
        {
            //Move data
            Array.Copy(mByteBuffer, 0, mByteBuffer, mReadPos, RemainReadCap);
            mWritePos = RemainReadCap;
            mReadPos = 0;
            
            if (RemainWriteCap >= size) return true;

            //Increase capacity
            int requestCap = (int)mCapacity + size;
            int newCap = requestCap > mCapacity * 2 ? requestCap : (int)(mCapacity * 2);
            byte[] newBuffer = new byte[newCap];
            Array.Copy(mByteBuffer, 0, newBuffer, 0, mWritePos);
            mByteBuffer = newBuffer;
            mCapacity = (uint)newCap;
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public void ShiftReadPos(int offset)
    {
        mReadPos += (uint)offset;
    }

    UInt16 ReverseByte(UInt16 data)
    {
        return (UInt16)((data & 0xFF00U) >> 8 |
                       ( data & 0x00FFU) << 8);
    }

    UInt32 ReverseByte(UInt32 data)
    {
        return (UInt32)(
                       (data & 0xFF000000U) >> 24 |
                       (data & 0x00FF0000U) >> 8 |
                       (data & 0x0000FF00U) << 8 |
                       (data & 0x000000FFU) << 24
                       );
    }

    //UInt64 ReverseByte(UInt64 data)
    //{
    //    return (UInt64)(
    //                   (data & 0xFF00000000000000UL) >> 56 |
    //                   (data & 0xFF00000000000000UL) >> 24 |
    //                   (data & 0x0000FF00U) << 8 |
    //                   (data & 0x000000FFU) << 24
    //                   );
    //}

    #region Write

    public bool Write(byte data)
    {
        if (!CheckReadCap(1))
            return false;
        try
        {
            mByteBuffer[mWritePos++] = data;
            return true;
        }
        catch (Exception e)
        {
            //TODO
            return false;
        }
    }

    public bool Write(byte[] data, int size)
    {
        if (!CheckWriteCap(size))
            return false;
        try
        {
            Array.Copy(data, 0, mByteBuffer, mWritePos, size);
            mWritePos += (uint)size;
            return true;
        }
        catch (Exception e)
        {
            //TODO
            return false;
        }
    }

    public bool Write(uint data)
    {
        if (!CheckWriteCap(sizeof(uint)))
            return false;
        if (isBigEndian)
            data = (uint)ReverseByte(data);
        byte[] bData = BitConverter.GetBytes(data);
        return Write(bData, bData.Length);
    }

    public bool Write(short data)
    {
        if (!CheckWriteCap(sizeof(short)))
            return false;
        if (isBigEndian)
            data = (short)ReverseByte((ushort)data);
        byte[] bData = BitConverter.GetBytes(data);
        return Write(bData, bData.Length);
    }

    public bool Write(NetMsgHead haed)
    {
        if (!Write(haed.mSize))
            return false;
        if (!Write(haed.mCmdID))
            return false;
        return true;
    }

    public bool Write(NetMsgDef msg)
    {
        if (!Write(msg.mMsgHead))
            return false;
        if (!Write(msg.mBtsData, msg.mBtsData.Length))
            return false;
        return true;
    }
    #endregion

    #region Read
    public bool Read(out byte data)
    {
        data = 0;
        if (!CheckReadCap(sizeof(byte)))
            return false;
        data = mByteBuffer[mReadPos++];
        return true;
    }

    public bool Read(byte[] data, int size)
    {
        if (!CheckReadCap(size))
            return false;
        try
        {
            Array.Copy(mByteBuffer, (int)mReadPos, data, 0, size);
            mReadPos += (uint)size;
            return true;
        }
        catch (Exception e)
        {
            //TODO
            return false;
        }
    }

    public bool Read(out short data)
    {
        data = 0;
        if (!CheckReadCap(sizeof(short)))
            return false;
        data = BitConverter.ToInt16(mByteBuffer, (int)mReadPos);
        if (isBigEndian)
            data = (Int16)ReverseByte((UInt16)data);
        mReadPos += sizeof(short);
        return true;
    }

    public bool Read(ref NetMsgHead head)
    {
        head = null;
        short cmdID;
        short size;
        if (!Read(out size))
            return false;
        if (!Read(out cmdID))
            return false;
        head = new NetMsgHead(cmdID, size);
        return true;
    }

    public bool Read(ref NetMsgDef msg)
    {
        msg = null;
        NetMsgHead head = null;
        if (!Read(ref head))
            return false;
        byte[] btsData = new byte[head.mSize];
        if (!Read(btsData, head.mSize))
            return false;
        msg = new NetMsgDef(head, btsData);
        return true;
    }
    #endregion
}
