//FollowState  跟随状态，状态的一种，子类
using UnityEngine;
using System.Collections;
namespace FSM
{
    /// <summary>
    /// 追踪状态
    /// </summary>
    public class StateFollow : FSMStateBase
    {
        CharacterController characterController;
        public StateFollow(FSMSystem fms, Transform trans)
        {
            this.fms = fms;
            this.name = "follow";
            this.transSelf = trans;
            transTarget = GameObject.FindGameObjectWithTag("Player").transform;
            characterController = transSelf.GetComponent<CharacterController>();
        }
        public override void action()
        {
            Animation animation = transSelf.GetComponent<Animation>();
            animation.CrossFade("run_forward");
            searchTheTarget(transSelf,transTarget);
            characterController.Move(transSelf.forward * Time.deltaTime * curSpeed);
            //Vector3 moveDirection = transNpc.TransformDirection(Vector3.Cross(Vector3.up, Vector3.left));
            //Vector3 velocity = moveDirection.normalized * curSpeed;
            //if (characterController != null)
            //{
            //    characterController.SimpleMove(velocity);
            //}
            //播放动画
            
        }

        public override void reason()
        {
            float dis = Vector3.Distance(transSelf.position, transTarget.position);
            if (dis > 6)
            {
                fms.doTransition(ETransition.FOLLOW_TO_PATROL);
            }
            else if (dis <= 3)//距离目标3米转到攻击状态
            {
                fms.doTransition(ETransition.FOLLOW_TO_ATTACK);
            }
        }
    }

}