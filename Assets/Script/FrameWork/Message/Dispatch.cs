// **********************************************************************
// Copyright (C) test
// Author: txz
// Date: 2020-04-13
// Desc: 
// **********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U3dFramework
{
    public interface IDispatcher<T>
    {
        void AddListener(int type, Action<T> listener);
    
        void RemoveListener(int type, Action<T> listener);
    
        void SendMessage(Message evt);
    
        void SendMessage(int type, params System.Object[] param);
    
        void Clear();
    }

    public class Dispatcher : Singleton<Dispatcher>
    {
        
        private Dictionary<int, Action> events = new Dictionary<int, Action>();
    
        public void AddListener(int type, Action listener)
        {
            if (listener == null)
            {
                Debug.LogError("AddListener: listener不能为空");
                return;
            }

            if (!events.ContainsKey(type))
            {
                events.Add(type, null);
            }
            events[type] += listener;

        }
    
        public void RemoveListener(int type, Action listener)
        {
            if (listener == null)
            {
                Debug.LogError("RemoveListener: listener不能为空");
                return;
            }
    
            // events[type] = (Action<T>)Delegate.Remove(events[type], listener);

            if (events.ContainsKey(type) && events[type] != null)
            {
                 events[type] -= listener;
            }
        }
    
        public void Clear()
        {
            events.Clear();
        }
    
        public void SendMessage(Message evt)
        {
            Action listenerDelegate;
            if (events.TryGetValue(evt.Type, out listenerDelegate))
            {
                try
                {
                    
                    if (listenerDelegate != null)
                    {
                        listenerDelegate();
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("SendMessage:"+evt.Type.ToString()+","+e.Message+","+e.StackTrace+" "+e);
                }
            }
        }
    
        public void SendMessage(int type, params System.Object[] param)
        {
            if (events.ContainsKey(type) && events[type] != null)
            {
                events[type]();
            }
        }
    }
}