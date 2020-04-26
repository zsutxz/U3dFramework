using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

internal class ObjectPool<T> where T : new()
{
    private readonly Stack<T> m_Stack = new Stack<T>();
    private readonly UnityAction<T> m_ActionOnGet;
    private readonly UnityAction<T> m_ActionOnRelease;

    public int countAll{get;private set;}
    public int countActivate {get{return countAll-countInactive;}}
    public int countInactive{get{return m_Stack.Count;}}

    //创建缓冲池，并提供从池子中获取，和释放到池子中的回调事件。
    public ObjectPool(UnityAction<T> actionOnGet,UnityAction<T> actionOnRelease)
    {
        m_ActionOnGet =  actionOnGet;
        m_ActionOnRelease = actionOnRelease;
    }

    public T Get()
    {
        T element;
        if(m_Stack.Count==0)
        {
            element = new T();
            countAll++;
        }
        else
        {
            element = m_Stack.Pop();
        }
        //从池子拿出钱，做一些初始化操作
        if(m_ActionOnGet!=null)
        {
            m_ActionOnGet(element);
        }
        return element;

    }

    //释放到池子
    public void Release(T element)
    {
        if(m_Stack.Count>0&& ReferenceEquals(m_Stack.Peek(),element))
        {
            Debug.LogError("Internal error, Trying to destroy obect that is already released to pool.");
        }

        if(m_ActionOnRelease!=null)
        {
            m_ActionOnRelease(element);
        }
        m_Stack.Push(element);
    }

    //GameObject 类型的缓冲次吃
}
