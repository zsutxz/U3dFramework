using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CEventListenerDelegate(CBaseEvent evt);

public class CEventDispatcher
{
    static CEventDispatcher instance;
    public static CEventDispatcher GetInstance()
    {
        if (instance == null)
        {
            instance = new CEventDispatcher();
        }
        return instance;
    }

    private Hashtable listeners = new Hashtable();

    public void AddEventListener(CEventType eventType, CEventListenerDelegate listener)
    {
        CEventListenerDelegate ceventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
        ceventListenerDelegate = (CEventListenerDelegate)Delegate.Combine(ceventListenerDelegate, listener);
        this.listeners[eventType] = ceventListenerDelegate;
    }

    public void RemoveEventListener(CEventType eventType, CEventListenerDelegate listener)
    {
        CEventListenerDelegate ceventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
        if (ceventListenerDelegate != null)
        {
            ceventListenerDelegate = (CEventListenerDelegate)Delegate.Remove(ceventListenerDelegate, listener);
        }
        this.listeners[eventType] = ceventListenerDelegate;
    }

    public void AddEventListener(string eventName, CEventListenerDelegate listener)
    {
        CEventListenerDelegate ceventListenerDelegate = this.listeners[eventName] as CEventListenerDelegate;
        //这个combine就相当于委托的+=    (整体的实现就是类似于观察者模式)
        ceventListenerDelegate = (CEventListenerDelegate)Delegate.Combine(ceventListenerDelegate, listener);
        this.listeners[eventName] = ceventListenerDelegate;
    }

    /// <summary>
    /// 使用枚举传递
    /// </summary>
    /// <param name="evt"></param>
    public void DispatchEvent(CBaseEvent evt)
    {
        CEventListenerDelegate ceventListenerDelegate = this.listeners[evt.Type] as CEventListenerDelegate;

        if (ceventListenerDelegate != null)
        {
            try
            {
                ceventListenerDelegate(evt);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(new string[]
                {
                    "Error dispatching event ",
                    evt.Type.ToString(),
                    ": ",
                    ex.Message,
                    " ",
                    ex.StackTrace
                }), ex);
            }
        }
    }

    /// <summary>
    /// 使用字符串传递
    /// </summary>
    /// <param name="evt"></param>
    public void DispatchStringEvent(CBaseEvent evt)
    {
        CEventListenerDelegate ceventListenerDelegate = this.listeners[evt.EventName] as CEventListenerDelegate;

        if (ceventListenerDelegate != null)
        {
            try
            {
                ceventListenerDelegate(evt);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat(new string[]
                {
                    "Error dispatching event ",
                    evt.Type.ToString(),
                    ": ",
                    ex.Message,
                    " ",
                    ex.StackTrace
                }), ex);
            }
        }
    }

    public void RemoveAll()
    {
        this.listeners.Clear();
    }
}