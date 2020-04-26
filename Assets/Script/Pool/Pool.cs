using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public class PoolItem : MonoBehaviour
    {

        protected bool m_Active = true;
        public virtual bool IsActive()
        {
            return m_Active;
        }
    }

    public class Pool<T> where T : PoolItem
    {

        private List<T> m_IdleList = new List<T>();
        private List<T> m_ActiveList = new List<T>();


        public T GetOneIdle(T perfab)
        {
            T t = default(T);
            if (m_IdleList.Count > 1)
            {
                t = m_IdleList[m_IdleList.Count - 1];
                m_IdleList.RemoveAt(m_IdleList.Count - 1);
            
            }
            else
            {
                t = GameObject.Instantiate<T>(perfab);
            }
            return t;
        }

        public void AddToActiveList(T t)
        {
            m_ActiveList.Add(t);
        }

        public void Recycle(T t)
        {
            t.gameObject.SetActive(false);
            m_IdleList.Add(t);
        
        }

        public void TestUnactiveToIdleList()
        {
            for (int i = m_ActiveList.Count - 1; i >= 0; i--)
            {
                T t = m_ActiveList[i];
                if (!t.IsActive())
                {
                    m_ActiveList.RemoveAt(i);
                    t.gameObject.SetActive(false);
                    Recycle(t);
                }
            }
        }

        public List<T> GetActiveList()
        {
            return m_ActiveList;
        }
    }


    public class PoolManyDim<K,V>  where V : PoolItem
    {
        private Dictionary<K, Pool<V> > m_Pools = new Dictionary<K, Pool<V> >();

        public V GetOneIdle(K k, V prefab)
        {
            Pool<V> p = null;
            m_Pools.TryGetValue(k, out p);
            if (p == null)
            {
                p = new Pool<V>();
                m_Pools.Add(k, p);
            }
            V v = p.GetOneIdle(prefab);
            p.AddToActiveList(v);
            return v;
        }

        public void TestUnactiveToIdleList()
        {
            foreach (Pool<V> p in m_Pools.Values)
            {
                p.TestUnactiveToIdleList();
            }
        }

    }


    public class DicionaryPool<K, V>
    {
        private Dictionary<K, List<V>> m_Data = new Dictionary<K, List<V>>();

        public V GetOne(K key)
        {
            V v = default(V);
            List<V> lt;
            m_Data.TryGetValue(key, out lt);
            if (lt == null)
            {
                lt = new List<V>();
                m_Data[key] = lt;
            }

            if (lt.Count > 0)
            {
                v = lt[lt.Count - 1];
                lt.RemoveAt(lt.Count - 1);
                return v;
            }
            return v;
        }


        public void Recycle(K k, V v)
        {

            List<V> lt = null;
            m_Data.TryGetValue(k, out lt);
            if (lt == null)
            {
                lt = new List<V>();
                m_Data[k] = lt;
            }
            lt.Add(v);
        }
    }
}