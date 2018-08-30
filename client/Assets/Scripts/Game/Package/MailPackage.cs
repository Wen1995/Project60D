using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;


public class NMessageInfo
{
    public long id = 0;
    public int type = 0;
    public ZombieInfo zombieInfo = null;
    public FightingInfo fightingInfo = null;
    public long time = 0;
    public bool isRead = false;

    public NMessageInfo(MessageInfo info)
    {
        id = info.Id;
        type = info.Type;
        zombieInfo = info.ZombieInfo;
        fightingInfo = info.FightingInfo;
        time = info.Time;
        isRead = info.IsRead;
    }
}
public class MailPackage : ModelBase {

    List<NMessageInfo> mailList = new List<NMessageInfo>();
    int unreadMailCount = 0;

    public void SetMail(TSCGetPageList res)
    {
        mailList.Clear();
        for(int i=0;i<res.MessageInfoCount;i++)
        {
            mailList.Add(new NMessageInfo(res.GetMessageInfo(i)));
        }
    }

    public void SetUnreadMailCount(int count)
    {
        unreadMailCount = count;
    }


    public List<NMessageInfo> GetMailList()
    {
        return mailList;
    }

    public int GetUnreadMailCount()
    {
        return unreadMailCount;
    }

	public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
