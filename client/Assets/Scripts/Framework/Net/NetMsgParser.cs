using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMsgParser {

    public static Object ParseNetMsg(NetMsgDef msg)
    {
        return ParseNetMsg(msg.mMsgHead.mCmdID, msg.mBtsData);
    }

    public static Object ParseNetMsg(short cmdID, byte[] data)
    {
        Object obj = null;
        switch (cmdID)
        {
            //TODO
        }

        return obj;
    }
}
