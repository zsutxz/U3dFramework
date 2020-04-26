using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CEventType
{
    GAME_OVER,
    GAME_WIN,
    PAUSE,
    GAME_DATA,
}

public class CBaseEvent
{
    protected Hashtable arguments;
    protected CEventType type = CEventType.GAME_WIN;
    protected object sender;
    protected string eventName;

    public CEventType Type
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
        }
    }

    public IDictionary Params
    {
        get
        {
            return this.arguments;
        }
        set
        {
            this.arguments = (value as Hashtable);
        }
    }

    public object Sender
    {
        get
        {
            return this.sender;
        }
        set
        {
            this.sender = value;
        }
    }

    public string EventName
    {
        get
        {
            return eventName;
        }

        set
        {
            eventName = value;
        }
    }

    public override string ToString()
    {
        return this.type + " { " + ((this.sender == null) ? "null" : this.sender.ToString()) + " } ";
    }

    public CBaseEvent Clone()
    {
        return new CBaseEvent(this.type, this.arguments, Sender);
    }

    public CBaseEvent(CEventType type, object sender)
    {
        Sender = sender;
        if (this.arguments == null)
        {
            this.arguments = new Hashtable();
        }
    }

    public CBaseEvent(CEventType type, Hashtable args, object sender)
    {
        this.Type = type;
        this.arguments = args;
        Sender = sender;
        if (this.arguments == null)
        {
            this.arguments = new Hashtable();
        }
    }

    public CBaseEvent(string eventName, object sender)
    {
        this.Type = type;
        this.EventName = eventName;
        Sender = sender;
        if (this.arguments == null)
        {
            this.arguments = new Hashtable();
        }
    }
}