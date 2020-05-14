using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using U3dFramework;

public class TestMsg : MonoBehaviour {
    // public Dispatcher disaptch;
    
    void Awake()
    {
        Dispatcher  disaptch = Dispatcher.Instance;
        //监听消息
        disaptch.AddListener((int)MsgType.START_UP, OnAwakeCall);
        disaptch.AddListener((int)MsgType.UPDATE, OnStartCall);
        //disaptch.AddListener((int)MsgType.UPDATE , delegate{ Debug.Log("start");});
        //发送不带参数广播
        //disaptch.Broadcast(MsgType.UPDATE_COMPLETE);
    }

    void Start()
    {
        Message test_msg = new Message((int)MsgType.START_UP);
        //发送消息
        Dispatcher.Instance.SendMessage(test_msg);
    }

    void OnDestroy()
    {
        //移除监听
        Dispatcher.Instance.RemoveListener((int)MsgType.START_UP,OnAwakeCall);
        Dispatcher.Instance.RemoveListener((int)MsgType.UPDATE,OnStartCall);
    }
    //消息回调


    Action OnAwakeCall = () => {
        Debug.Log("awake");
    };

    public void OnStartCall()
    {
        Debug.Log("start");
    }
}