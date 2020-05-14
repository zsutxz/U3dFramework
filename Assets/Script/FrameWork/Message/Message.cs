// **********************************************************************
// Copyright (C) test
// Author: txz
// Date: 2020-04-13
// Desc: 
// **********************************************************************
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace U3dFramework
{
       /// <summary>
    /// 消息的类型  
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 启动
        /// </summary>
        START_UP = 1000,
        /// <summary>
        /// 解压
        /// </summary>
        UNPACK,
        /// <summary>
        /// 更新
        /// </summary>
        UPDATE,
        /// <summary>
        /// 更新完成
        /// </summary>
        UPDATE_COMPLETE,
    }
    
    
    /// <summary>
    /// 战斗的类型
    /// </summary>
    public enum BattleEvent
    {
        /// <summary>
        /// 攻击
        /// </summary>
        Attack = 10000,
    }
    
    public interface IMessage
    {
        /// <summary>
        /// 事件类型，Key
        /// </summary>
        int Type { get; set; }
    
        /// <summary>
        /// 发送者
        /// </summary>
        System.Object Sender { get; set; }
    
        /// <summary>
        /// 参数
        /// </summary>
        System.Object[] Params { get; set; }
    
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <returns></returns>
        string ToString();
    }

    public class Message : IMessage {
        public int Type { get; set; }
    
        public System.Object[] Params { get; set; }
    
        public System.Object Sender { get; set; }
    
        public override string ToString()
        {
            string arg = null;
            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i++)
                {
                    if ((Params.Length > 1 && Params.Length - 1 == i) || Params.Length == 1)
                    {
                        arg += Params[i];
                    }
                    else
                    {
                        arg += Params[i] + " , ";
                    }
                }
            }
    
            return Type + " [ " + ((Sender == null) ? "null" : Sender.ToString()) + " ] " + " [ " + ((arg == null) ? "null" : arg.ToString()) + " ] ";
        }
    
        public Message Clone()
        {
            return new Message(Type, Params, Sender);
        }
    
        public Message(int type)
        {
            Type = type;
        }
    
        public Message(int type, params System.Object[] param)
        {
            Type = type;
            Params = param;
        }
    
        public Message(int type, System.Object sender, params System.Object[] param)
        {
            Type = type;
            Params = param;
            Sender = sender;
        }
    }
}
