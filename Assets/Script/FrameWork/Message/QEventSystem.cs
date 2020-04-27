using System;
using System.Collections.Generic;
//事件接口
public delegate void OnEvent(int key, params object[] param);
public class QEventSystem : Singleton<QEventSystem>
{
    private bool mIsRecycled = false;
    private readonly Dictionary<int, ListenerWrap> mAllListenerMap = new Dictionary<int, ListenerWrap>(50);

    // public bool IsRecycled { get; set; }

    public QEventSystem() { }

    #region 内部结构

    private class ListenerWrap
    {
        private LinkedList<OnEvent> mEventList;  //事件列表
        //发送消息时所调用的函数
        public bool Fire(int key, params object[] param)
        {
            if (mEventList == null) //判断有没有事件
            {
                return false;
            }

            var next = mEventList.First;
            OnEvent call = null;
            LinkedListNode<OnEvent> nextCache = null;
            //循环调用消息对应的事件
            while (next != null)
            {
                call = next.Value;
                nextCache = next.Next;
                call(key, param);

                next = next.Next ?? nextCache; //??空合并运算符: 当next.Next的值为空则返回nextCache,否则返回next.Next,前面不能使条件,只能判断值
            }

            return true;
        }

        public bool Add(OnEvent listener)
        {
            if (mEventList == null)
            {
                mEventList = new LinkedList<OnEvent>();
            }

            if (mEventList.Contains(listener))
            {
                return false;
            }

            mEventList.AddLast(listener);
            return true;
        }

        public void Remove(OnEvent listener)
        {
            if (mEventList == null)
            {
                return;
            }

            mEventList.Remove(listener);
        }

        public void RemoveAll()
        {
            if (mEventList == null)
            {
                return;
            }

            mEventList.Clear();
        }
    }

    #endregion

    #region 功能函数

    public bool Register<T>(T key, OnEvent fun) where T : IConvertible
    {
        var kv = key.ToInt32(null);
        ListenerWrap wrap;
        if (!mAllListenerMap.TryGetValue(kv, out wrap)) //判断之前是否已经注册过信息
        {
            wrap = new ListenerWrap();
            mAllListenerMap.Add(kv, wrap);
        }

        if (wrap.Add(fun))  //添加响应事件
        {
            return true;
        }

        //Log.W("Already Register Same Event:" + key);
        return false;
    }
    //移除指定的事件
    public void UnRegister<T>(T key, OnEvent fun) where T : IConvertible
    {
        ListenerWrap wrap;
        if (mAllListenerMap.TryGetValue(key.ToInt32(null), out wrap))
        {
            wrap.Remove(fun);
        }
    }
    //移除注册的消息
    public void UnRegister<T>(T key) where T : IConvertible
    {
        ListenerWrap wrap;
        if (mAllListenerMap.TryGetValue(key.ToInt32(null), out wrap))
        {
            wrap.RemoveAll();
            wrap = null;

            mAllListenerMap.Remove(key.ToInt32(null));
        }
    }

    public bool Send<T>(T key, params object[] param) where T : IConvertible
    {
        int kv = key.ToInt32(null);
        ListenerWrap wrap;
        if (mAllListenerMap.TryGetValue(kv, out wrap))
        {
            return wrap.Fire(kv, param);
        }
        return false;
    }

    public void OnRecycled()
    {
        mAllListenerMap.Clear();
    }

    #endregion


    #region 高频率使用的API,封装成静态方法,提供给外部调用
    //发送调用事件的信息
    // public static bool SendEvent<T>(T key,params object[] param) where T : IConvertible
    // {
    //     return Instance.Send(key, param);
    // }
    // //注册事件
    // public static bool RegisterEvent<T>(T key, OnEvent fun) where T : IConvertible
    // {
    //     return Instance.Register(key, fun);
    // }
    // //取消注册事件
    // public static void UnRegisterEvent<T>(T key, OnEvent fun) where T : IConvertible
    // {
    //     Instance.UnRegister(key, fun);
    // }
    #endregion
}