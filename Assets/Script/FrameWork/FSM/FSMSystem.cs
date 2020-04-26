
//FSMsystem   控制系统
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    /// <summary>
    /// 状态管理器
    /// 
    /// 状态的增加
    /// 状态的删除
    /// 状态的修改
    /// 状态的查找
    /// </summary>
    public class FSMSystem
    {
        private List<FSMStateBase> listState = new List<FSMStateBase>();//存储状态的容器
        private FSMStateBase curState;//当前状态，
        public FSMStateBase CurState { get { return curState; } }
        public FSMSystem()
        {
           
        }
        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void addState(FSMStateBase state)//基类的引用
        {
            if (state == null)
            {
                return;
            }
            if (listState.Count == 0)
            {
                listState.Add(state);
                curState = state;//如果还没有存其它状态，那么把第一个当成默认状态
                return;
            }
            if (!listState.Contains(state))//保证没有重复的状态
            {
                listState.Add(state);
            }

        }
        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="stateName">状态的名字</param>
        public void deleteState(string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return;
            }
            for (int i = 0; i < listState.Count; i++)
            {
                if (listState[i].Name == stateName)
                {
                    listState.Remove(listState[i]);
                    //listState.RemoveAt(i);
                    break;
                }
            }
        }
        /// <summary>
        /// 转换状态
        /// </summary>
        /// <param name="transition">条件</param>
        public void doTransition(ETransition transition)
        {
            if (transition == ETransition.NULL)
            {
                return;
            }
            string newStateName=curState.getOutState(transition);//获取当前状态要装换到的下一个状态名字
            if (string.IsNullOrEmpty(newStateName))
            {
                return;
            }
            //转换状态
            for (int i = 0; i < listState.Count; i++)
            {
                if (newStateName.Equals(listState[i].Name))
                {
                    curState.doBeforeLeaving();//当前状态离开时候要做的事情
                    curState=listState[i];//更换当前状态
                    curState.doBeforeEntering();//进入当前状态做的事情
                    break;
                }
            }
        }
    }
}
