using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CEventDispatcher.GetInstance().AddEventListener("开始触发", MyTestFunc);
        CEventDispatcher.GetInstance().DispatchStringEvent(new CBaseEvent("开始触发", "这是重要数值123"));
    }

    public void MyTestFunc(CBaseEvent evt)
    {
        print("我是参数abc:" + evt);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
