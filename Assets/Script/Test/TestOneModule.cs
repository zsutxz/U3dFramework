using System;
using System.Collections.Generic;
using UnityEngine;

using U3dFramework;

 

public class TestOneModule : BaseModule{

    public int Gold{  get;    private set;  }

 

    public TestOneModule(){

        this.AutoRegister = true;

    }

 

    protected override void OnLoad(){

        Dispatcher.Instance.AddListener((int)MsgType.START_UP, OnCall);

        base.OnLoad();

    }

 

    protected override void OnRelease(){

        Dispatcher.Instance.RemoveListener((int)MsgType.START_UP, OnCall);

        base.OnRelease();

    }

 

    Action OnCall = () => {
        Debug.Log("oncall");
    };

    private void UpdateGold(Message message){

        //int gold = (int)message["gold"];

        Message test_msg = new Message((int)MsgType.START_UP);
        //发送消息
        Dispatcher.Instance.SendMessage(test_msg);
    }

}