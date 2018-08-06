package com.game.framework.console.disruptor;

import com.lmax.disruptor.EventFactory;

public class ObjectEvent
{
	
	private Object obj;

	public Object getObject() 
	{
		return obj;
	}

	public void setObject(Object obj) 
	{
		this.obj = obj;
	}


	public final static EventFactory<ObjectEvent> EVENT_FACTORY = new EventFactory<ObjectEvent>() 
	{
		public ObjectEvent newInstance() 
		{
			return new ObjectEvent();
		}
	};
}