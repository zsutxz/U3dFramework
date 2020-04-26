//FSMStateBase  基类
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//各种集合，数据结构
namespace FSM
{
    public enum ETransition//状态机中的连线（转换条件）
    {
        NULL,//不需要转换
        PATROL_TO_FOLLOW,//巡逻到跟随
        PATROL_TO_ATTACK,//巡逻到攻击
        FOLLOW_TO_ATTACK,//跟随到攻击
        FOLLOW_TO_PATROL,//跟随到巡逻
        ATTACK_TO_FOLLOW,//攻击到跟随
        ATTACK_PATROL,//攻击到巡逻
        

    }
    public abstract class FSMStateBase
    {
        protected float curRotateSpeed = 20;//角速度
        protected float curSpeed = 10;//移动速度

        protected FSMSystem fms;//状态机管理器
        protected Transform transTarget;//追踪的目标
        protected Transform transSelf;//自己
        protected string name;//状态的名字
        public string Name { get { return name; } }
        protected Dictionary<ETransition, string> map = new Dictionary<ETransition, string>();//装换条件和状态的名字
        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="stateName"></param>
        public void addTransition(ETransition transition, string stateName)
        {
            if (transition == ETransition.NULL || string.IsNullOrEmpty(stateName))
            {
                return;
            }
            if (!map.ContainsKey(transition))
            {
                map.Add(transition,stateName);
            }
        }

        /// <summary>
        /// 删除条件
        /// </summary>
        /// <param name="transition"></param>
        public void deleteTransition(ETransition transition)
        {
            if (transition == ETransition.NULL)
            {
                return;
            }
            if (map.ContainsKey(transition))
            {
                map.Remove(transition);
            }

        }
        /// <summary>
        /// 旋转当前位置到目标位置，重点----经常会用到
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="aim"></param>
        public void searchTheTarget(Transform cur, Transform aim)
        {
            Quaternion destRotation;
            Vector3 tmpCur=cur.position;
            Vector3 tmpAim = aim.position;
            tmpCur.y = 0.0f;
            tmpAim.y = 0.0f;
            Vector3 relativePos = tmpAim - tmpCur;

            destRotation = Quaternion.LookRotation(relativePos);
            cur.rotation = Quaternion.Slerp(cur.rotation, destRotation, Time.deltaTime * curRotateSpeed);//插值运算
        }
        /// <summary>
        /// 获取新的状态，如果有状态过渡
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        public string getOutState(ETransition transition)
        {
            if (map.ContainsKey(transition))
            {
                return map[transition];
            }
            else {
                return null;
            }
        }
        //进入当前状态之前做的事情（已经由上一个状态更新到了另一个状态了）
        public virtual void doBeforeEntering() { }
        //离开当前状态之前做的事情（还没有更新到新状态执行的函数）
        public virtual void doBeforeLeaving() { }
        public abstract void action();//控制行为（循环函数Update,控制物体的移动）
        public abstract void reason();//检测转换（条件检测）

    }
}