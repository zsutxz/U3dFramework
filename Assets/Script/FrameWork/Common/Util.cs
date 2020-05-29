using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace U3dFramework
{
    public class Util
    {
        /// <summary>
        /// 输入百分比返回是否命中概率
        /// </summary>
        public static bool Percent(int per)
        {
            return Random.Range (0, 100) <= per;
        }

        public static object GetRandomValueFrom(object[] values)
        {
            return values[Random.Range(0, values.Length)];
        }


        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }
        //两种使用方法
        //var intRandomValue = GetRandomValueFrom(new int[]{1,2,3});
        //var floatRandomValue = (float)GetRandomValueFrom(new float[]{ 0.1f,0.2f });
        //var floatRandomValue = (float)GetRandomValueFrom(0.1f,0.2f);
    }
}