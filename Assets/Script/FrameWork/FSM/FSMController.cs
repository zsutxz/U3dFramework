using UnityEngine;
using System.Collections;

namespace FSM
{
    //FSMController主函数

    public class FSMController : MonoBehaviour
    {
        private FSMSystem fms;///FSM系统，用来管理状态
        public Transform[] points;//自己玩耍时的四个移动点
        public Transform firePoint;//子弹位置
        public Transform fireLight;
        // Use this for initialization
        void Start () {  
            GameObject curGo = GameObject.FindGameObjectWithTag("PointsPatrol");//
            int count=curGo.transform.childCount;
            points=new Transform[count];
            int index=0;
            foreach (Transform a in curGo.transform)
            {
                points[index] = a;
                index++;
            }
            makeFMS();//状态机系统的初始化
        }
        
        // Update is called once per frame
        //程序的核心部分，通过多态机制，切换实例化不同的抽象基类的子类，实现调用不同子类中的action和reason方法
        void Update () {
            if (fms.CurState!=null)
            {
                fms.CurState.action();
                fms.CurState.reason();
            }
            
        }
        private void makeFMS()
        {
            fms = new FSMSystem();//状态机管理系统
            // StatePatrol statePatrol = new StatePatrol(fms, points,transform);//巡逻状态
            // statePatrol.addTransition(ETransition.PATROL_TO_FOLLOW, "follow");
            // statePatrol.addTransition(ETransition.PATROL_TO_ATTACK, "attack");

            StateFollow stateFollow = new StateFollow(fms, transform);//跟随状态
            stateFollow.addTransition(ETransition.FOLLOW_TO_ATTACK, "attack");
            stateFollow.addTransition(ETransition.FOLLOW_TO_PATROL, "patrol");

            StateAttack stateAttack = new StateAttack(fms, transform, firePoint, fireLight);//攻击状态
            stateAttack.addTransition(ETransition.ATTACK_TO_FOLLOW, "follow");
            stateAttack.addTransition(ETransition.ATTACK_PATROL, "patrol");

            // fms.addState(statePatrol);
            fms.addState(stateFollow);
            fms.addState(stateAttack);

        }
    }
}
