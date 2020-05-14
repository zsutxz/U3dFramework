using System.Collections;
using System.Collections.Generic;
using U3dFramework;
using UnityEngine;

#region 全局委托Global delegate

public delegate void StateChangeEvent (Object sender, EnumObjectState newState, EnumObjectState oldState);

public delegate void MessageEvent (Message message);

public delegate void OnTouchEventHandle (GameObject sender, object _args, params object[]  _params);

public delegate void PropertyChangedHandle (BaseActor actor, int id, object oldValue, object newValue);

#endregion

public enum EnumObjectState {

         
    None,

        Initial,

        Loading,

        Ready,

         Disabled,

        Closing

}

   
public enum EnumPropertyType : int {    
    RoleName = 1,    //立钻哥哥：角色名
         Sex,     //性别
        RoleID,     //Role ID
         Gold,     //宝石（元宝）
        Coin,     //金币（铜板）
        Level,     //等级
        Exp,     //当前经验
        AttackSpeed,     //攻击速度
        HP,     //当前HP
        HPMax,     //生命最大值
         Attack,     //普通攻击（点数）
         Water,     //水系攻击（点数）
        Fire,     //火系攻击（点数）
}

public enum EnumActorType {

    None = 0,
    Role,
    Monster,
    NPC,
}

   
public enum EnumSceneType {

           
    None = 0,

           StartGame,

           LoadingScene,

           LoginScene,

        MainScene,

        CopyScene,

        PVPScene,

        PVEScene,
}
public enum EnumUIType : int {

    None = -1,

    TestOne,

    TestTwo,

}

public class Defines : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}