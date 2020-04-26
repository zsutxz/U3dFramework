using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    // public Camera camera;
    public Material material;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Rotation()
    {
        //旋转到给定的值，改变的是欧拉角
        transform.DORotate(new Vector3(0, 0, 60), 2);
        //旋转到给定的值，改变的是四元数
        transform.DORotateQuaternion(new Quaternion(0.1f, 0.1f, 0.1f, 0.1f), 2);

        //在给定时间内，平滑的让自身的z轴正方向指向目标点
        transform.DOLookAt(new Vector3(0, 0, 0), 2);
        transform.DOScale(new Vector3(2, 2, 2), 2);
        transform.DOScaleX(3, 2);
    }

    void Punch()
    {
        // 第一个参数 punch：表示方向及强度
        // 第二个参数 duration：表示动画持续时间
        // 第三个参数 vibrato：震动次数
        // 第四个参数 elascity: 这个值是0到1的
        //                     当为0时，就是在起始点到目标点之间运动
        //                     不为0时，会把你赋的值乘上一个参数，作为你运动方向反方向的点，物体在这个点和目标点之间运动

        // transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 0.1f);
        // transform.DOPunchRotation(new Vector3(0, 90, 0), 2, 2, 0.1f);
        // transform.DOPunchScale(new Vector3(2, 2, 2), 2, 2, 0.1f);

        transform.DOPunchPosition(new Vector3(20, 0, 0), 2, 2, 0.3f);
    }

    void shake()
    {
        // 参数：持续时间，力量，震动，随机性，淡出
        // 力量：实际就是震动的幅度,可以理解成相机施加的力的大小 使用Vector3可以选择每个轴向不同的强度
        // 震动：震动次数
        // 随机性：改变震动方向的随机值（大小：0~180）
        // 淡出：就是运动最后是否缓慢移动回到原本位置

        //transform.DOShakePosition(1, 5, 10, 50, true);
        //transform.DOShakeRotation(3);

        transform.DOShakeScale(3);

    }

    void Blend()
    {
        //    带Blend名称的方法，允许混合动画
        //    原本同时执行两个Move方法，只会执行最新的一个动画命令
        //    例如：
        //     transform.DOMove(Vector3.one, 2);
        //     transform.DOMove(Vector3.one * 2, 2);
        //    结果是物体运动到了（2,2,2）坐标上

        //    DOBlendableMoveBy方法有两个特点
        //    1）允许多个同时执行
        //    例如：
        transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        transform.DOBlendableMoveBy(new Vector3(-1, 0, 0), 1);
        //     假设其实点为（0,0,0），最后动画停止时的坐标就是（0,1,1）
        //    2）它是增量动画
        transform.DOBlendableMoveBy(new Vector3(1, 1, 1), 1);
        // 假设其实点为（1,1,1），最后动画停止时的坐标就是（2,2,2）
        // 它的参数不是目标点，而是要移动的量

    }

    void ChangeCamera()
    {

        //1）调整屏幕视角的宽高比 第一个参数是宽高的比值
        //camera.DOAspect(0.6f, 2);

        //2）改变相机background参数的颜色
        //camera.DOColor(Color.blue, 2);

        //3）改变相机近切面的值
        //camera.DONearClipPlane(200, 2);

        //4）改变相机远切面的值
        //camera.DOFarClipPlane(2000, 2);

        //5）改变相机FOV的值
        //camera.DOFieldOfView(30, 2);

        //6）改变相机正交大小
        //camera.DOOrthoSize(10, 2);

        //7）按照屏幕像素计算的显示范围
        //camera.DOPixelRect(new Rect(0f, 0f, 600f, 500f), 2);

        //8）按照屏幕百分比计算的显示范围
        //camera.DORect(new Rect(0.5f, 0.5f, 0.5f, 0.5f), 2);

        // 9）相机震动
        //   相机震动效果 参数：持续时间，力量，震动，随机性，淡出
        //   力量：实际就是震动的幅度,可以理解成相机施加的力的大小 使用Vector3可以选择每个轴向不同的强度
        // 震动：震动次数
        // 随机性：改变震动方向的随机值（大小：0~180）
        // 淡出：就是运动最后是否缓慢移动回到原本位置
        GetComponent<Camera>().DOShakePosition(1, 10, 10, 50, false);
    }

    void ChangeMaterial()
    {
        material = gameObject.GetComponent<Material>();
        //1）改变颜色
        material.DOColor(Color.black, 2);

        //2）按照shader的属性名，修改颜色
        material.DOColor(Color.clear, "_Color", 2);


    }

    void ChangeText()
    {
        text.DOColor(Color.black, 2);
        text.DOFade(0, 2);
        text.DOBlendableColor(Color.black, 2);
    }

    public void MakeSequence()
    {
        Sequence quence = DOTween.Sequence();

        //1）添加动画到队列中
        quence.Append(transform.DOMove(Vector3.one, 2));

        //2）添加时间间隔
        quence.AppendInterval(1);

        //   3）按时间点插入动画
        //     第一个参数为时间，此方法把动画插入到规定的时间点
        //     以这句话为例，它把DORotate动画添加到此队列的0秒时执行，虽然它不是最先添加进队列的
        quence.Insert(0, transform.DORotate(new Vector3(0, 0, 90), 1));

        //   4）加入当前动画
        //     Join会加入和让动画与当前正在执行的动画一起执行
        //     如下两行代码，DOMove会和DOScale一起执行
        quence.Append(transform.DOScale(new Vector3(2, 2, 1), 2));
        quence.Join(transform.DOMove(Vector3.zero, 2));

        //   5）预添加动画
        //     预添加 会直接添加动画到Append的前面，也就是最开始的时候
        quence.Prepend(transform.DOScale(Vector3.one * 0.5f, 1));
        // 这里需要特别说一下预添加的执行顺序问题
        // 它这里也采取了队列的性质，不过，预添加与原本的的队列相比是一个反向队列
        // 例如：
        //  Sequence quence = DOTween.Sequence();
        //  quence.Append(transform.DOMove(Vector3.one, 2));
        //  quence.Prepend(transform.DOMove(-Vector3.one*2, 2));
        //  quence.PrependInterval(1);
        //  执行顺序是 PrependInterval----Prepend-----Append
        //  就是最后添加的会在队列的最顶端

        //   6）预添加时间间隔
        //quence.PrependInterval(1);

        // 回调函数

        //    1）预添加回调
        //     quence.PrependCallback(PreCallBack);

        //    2）在规定的时间点加入回调
        //     quence.InsertCallback(0, InsertCallBack);

        //    3）添加回调
        //     quence.AppendCallback(CallBack);
    }

    public void MakeTweener()
    {
        TweenParams para = new TweenParams();

        //   1）设置动画循环 
        //     第一个参数是循环次数  -1代表无限循环
        //     第二个参数是循环方式 
        //      Restart  重新开始  
        //      Yoyo   从起始点运动到目标点，再从目标点运动回来，这样循环 
        //      Incremental   一直向着运动方向运动
        para.SetLoops(-1, LoopType.Yoyo);

        //   2）设置参数
        transform.DOMove(Vector3.one, 2).SetAs(para);

        //   3)设置自动杀死动画
        transform.DOMove(Vector3.one, 1).SetAutoKill(true);

        //   4)from补间
        //     例如;
        transform.DOMove(Vector3.one, 2).From(true);
        //   From参数 isRelative(相对的)：
        //    为true，传入的就是偏移量，即当前坐标 + 传入值 = 目标值
        //    为falese，传入的就是目标值，即传入值 = 目标值

        //   5)设置动画延时 
        transform.DOMove(Vector3.one, 2).SetDelay(1);

        //   6）设置动画运动以速度为基准
        //      例如：
        transform.DOMove(Vector3.one, 1).SetSpeedBased();
        //   使用SetSpeedBased时，移动方式就变成以速度为基准
        //   原本表示持续时间的第二个参数，就变成表示速度的参数，每秒移动的单位数

        //   7）设置动画ID
        transform.DOMove(Vector3.one, 2).SetId("Id");

        //   8）设置是否可回收
        //     为true的话，动画播放完会被回收，缓存下来，不然播完就直接销毁
        transform.DOMove(Vector3.one, 2).SetRecyclable(true);

        //   9）设置动画为增量运动
        //       例如：
        transform.DOMove(Vector3.one, 2).SetRelative(true);
        // SetRelative参数 isRelative(相对的)：
        // 为true，传入的就是偏移量，即当前坐标 + 传入值 = 目标值 为falese，传入的就是目标值，即传入值 = 目标值

        //   10）设置动画的帧函数
        //    例如：
        transform.DOMove(Vector3.one, 2).SetUpdate(UpdateType.Normal, true);
        //  第一个参数 UpdateType :选择使用的帧函数
        //  UpdateType.Normal:更新每一帧中更新要求。 
        //  UpdateType.Late:在LateUpdate调用期间更新每一帧。 
        //  UpdateType.Fixed:使用FixedUpdate调用进行更新。 
        //  UpdateType.Manual:通过手动DOTween.ManualUpdate调用进行更新。
        //  第二个参数：为TRUE，则补间将忽略Unity的Time.timeScale
    }

    private IEnumerator Wait()
    {
        Tweener _tweener = transform.DOLocalMoveX(200, 1);

        //   //1)等待动画执行完
        yield return _tweener.WaitForCompletion();

        //   2）等待指定的循环次数
        //     参数为执行次数，等待传入的循环次数后，继续执行
        //     若是传入的次数大于动画的循环次数 则在动画结束时继续执行
        // yield return _tweener.WaitForElapsedLoops(2);

        // //   3）等待动画被杀死
        //     yield return _tweener.WaitForKill();

        //   4）等待动画执行指定时间
        //     参数为时间，动画执行传入的时间之后或动画执行完毕，继续执行
        //yield return _tweener.WaitForPosition(0.1f);

        //   5）等待动画回退
        //     以下情况会继续执行函数
        //     使用DORestart重新播放时
        //     使用Rewind倒播动画完成时
        //     使用DOFlip翻转动画完成时
        //     使用DOPlayBackwards反向播放动画完成时
        //yield return _tweener.WaitForRewind();

        //   6）等待Start执行后继续执行
        // yield return _tweener.WaitForStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            //Debug.Log("Press T!");
            StartCoroutine(Wait());

            //MakeTweener();
        }
        //Input();
    }

}
