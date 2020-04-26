using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//针对List的缓冲， 提供ListPool。
internal static class ListPool<T> 
{
    private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null,Clear);
    static void Clear(List<T> l)
    {
        l.Clear();
    }

    public static List<T> GetT()
    {
        return s_ListPool.Get();
    }
    public static void Release(List<T> toRelease)
    {
        s_ListPool.Release(toRelease);
    }

}
