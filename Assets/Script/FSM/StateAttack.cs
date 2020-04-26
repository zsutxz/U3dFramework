//AttackState   攻击状态，状态的一种，子类
using UnityEngine;
using System.Collections;
namespace FSM
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public class StateAttack : FSMStateBase
    {
        CharacterController characterController;//这个组件专门控制角色移动

        private float lastFire = -1;
        private float frequency = 0.1f;
        public GameObject prefabBullet;
        public Transform transFirePoint;//发射点
        public Transform transFireLight;//发射火光
        public Transform[] transBullets;
        public StateAttack(FSMSystem fms, Transform trans, Transform transFirePoint, Transform transFireLight)
        {
            this.fms = fms;
            this.name = "attack";//状态名字就叫攻击
            this.transSelf = trans;
            transTarget = GameObject.FindGameObjectWithTag("Player").transform;
            characterController = transSelf.GetComponent<CharacterController>();
            this.transFirePoint = transFirePoint;
            this.transFireLight = transFireLight;
            prefabBullet = Resources.Load("NewPrefabs/bullet") as GameObject;
            GameObject goBullet=GameObject.Find("Bullets_Monster1");
            if(goBullet!=null)
            {
                int num = goBullet.transform.childCount;
                transBullets=new Transform[num];
                int index=0;
                foreach(Transform t in goBullet.transform)
                {
                    transBullets[index] = t;
                    index++;
                }
            }
            
        }
        public override void action()
        {
            searchTheTarget(transSelf,transTarget);
            //characterController.Move(transSelf.forward * Time.deltaTime * curSpeed);
            Animation animation = transSelf.GetComponent<Animation>();
            animation.CrossFade("idle");//播放战力开火动画，（老版本动画播放模式）
            if (Time.time > lastFire + frequency)
            {
                transFireLight.gameObject.SetActive(true);
                lastFire = Time.time;
                //GameObject curGo = GameObject.Instantiate(prefabBullet, transFirePoint.position, transFirePoint.rotation) as GameObject;//以世界旋转为参考
                Transform curBullet = getBullet();
                if (curBullet == null) return;
                curBullet.gameObject.SetActive(true);
                curBullet.position = transFirePoint.position;
                curBullet.rotation = transFirePoint.rotation;
                // RaycastHit hitInfo;
                // Bullet b = curBullet.GetComponent<Bullet>();
                // //射线碰撞检测
                // if (Physics.Raycast(curBullet.position, curBullet.forward, out hitInfo))
                // {
                //     if (b)
                //     {
                //         b.distanceMove = hitInfo.distance;
                //     }
                //     //施加力，减少生命，播放音乐
                //     //Vector3 force = transform.forward * 10;
                //     //if (hitInfo.rigidbody)
                //     //{
                //     //    hitInfo.rigidbody.AddForceAtPosition(force, hitInfo.point, ForceMode.Impulse);
                //     //}

                //     //if (health)
                //     //{
                //     //    // Apply damage
                //     //    // health.OnDamage(damagePerSecond / frequency, -spawnPoint.forward);
                //     //}

                // }
                // else {
                //     if (b)
                //     {
                //         b.distanceMove = 1000;
                //     }
                // }
            }
        }

        public override void reason()
        {
            float dis=Vector3.Distance(transSelf.position, transTarget.position);
            if (dis > 3 && dis < 6)//
            {
                fms.doTransition(ETransition.ATTACK_TO_FOLLOW);
            }
            else if(dis>6)
            {
                fms.doTransition(ETransition.ATTACK_PATROL);
            }
        }
        public Transform getBullet()
        {
            for (int i = 0; i < transBullets.Length; i++)
            {
                if (!transBullets[i].gameObject.activeInHierarchy)
                { 
                    return transBullets[i];
                }
            }
            return null;
        }
    }

}