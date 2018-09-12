package com.nkm.framework.console.disruptor;

import java.util.ArrayList;
import java.util.List;
import com.google.protobuf.MessageLite;
import com.google.protobuf.MessageOrBuilder;
import com.google.protobuf.Parser;
import com.google.protobuf.TextFormat;
import io.netty.channel.Channel;

public class TPacket 
{
	Long uid;
	
	Short cmd;
	
	byte[] buffer;
	
	long receiveTime;
	
	Channel channel;
	
	Long groupId;
	
	Object data;//自定义数据
	
	Object mark;//标记
	
	List<Long> receivers;
	
	boolean isInner;
	
	public TPacket()
	{
		
	}

	public Short getCmd() {
		return cmd;
	}

	public void setCmd(int cmd) {
		this.cmd = (short)cmd;
	}

	public byte[] getBuffer() {
		return buffer;
	}

	public void setBuffer(byte[] buffer) {
		this.buffer = buffer;
	}

	public Object getData() {
		return data;
	}

	public void setData(Object data) {
		this.data = data;
	}

	public Long getUid() {
		return uid;
	}

	public void setUid(Long uid) {
		this.uid = uid;
	}
	
	public int getBufferSize() {
		return (buffer == null ? 0 : buffer.length);
	}

	public long getReceiveTime() {
		return receiveTime;
	}

	public void setReceiveTime(long receiveTime) {
		this.receiveTime = receiveTime;
	}

	public Channel getChannel() {
		return channel;
	}

	public void setChannel(Channel channel) {
		this.channel = channel;
	}
	
	public Long getGroupId() {
        return groupId;
    }

    public void setGroupId(Long groupId) {
        this.groupId = groupId;
    }

    public MessageLite parseProtobuf(Parser<MessageLite> parser) 
	{
        try 
        {
			return parser.parseFrom(buffer, 0, buffer.length);
		}
        catch (Exception e) {
        	return null;
	    }
	}
	
	public String toString(Parser<MessageLite> parser) 
	{
		String bufferJson = TextFormat.printToUnicodeString((MessageOrBuilder) parseProtobuf(parser));
		return String.format("uid[{}] send {cmd: {}, buffer: {}}", uid, cmd, bufferJson);
	}

	public Object getMark() {
		return mark;
	}

	public void setMark(Object mark) {
		this.mark = mark;
	}
	
	public void addReceivers(Long... uids)
	{
		if(receivers == null)
		{
			receivers = new ArrayList<>();
		}
		for (int i = 0; i < uids.length; i++) 
		{
			receivers.add(uids[i]);
		}
	}

	public List<Long> getReceivers() 
	{
		return receivers;
	}

	public boolean isInner() {
		return isInner;
	}

	public void setInner(boolean isInner) {
		this.isInner = isInner;
	}
	
}
