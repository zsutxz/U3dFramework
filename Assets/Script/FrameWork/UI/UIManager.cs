// using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }


            return _instance;
        }
    }

    private Dictionary<string, string> panelPathDict;
    private Dictionary<string, BasePanel> panelDict;
    private Stack<BasePanel> panelStack;

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    public void PushPanel(string panelType)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        //停止上一个界面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panelStack.Push(panel);
        panel.OnEnter();
    }

    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0)
        {
            return;
        }

        //退出栈顶面板
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        //恢复上一个面板
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }

    }

    private BasePanel GetPanel(string panelType)
    {

        if (panelDict == null)
        {
            panelDict = new Dictionary<string, BasePanel>();
        }

        BasePanel panel = panelDict.GetValue(panelType);

        //如果没有实例化面板，寻找路径进行实例化，并且存储到已经实例化好的字典面板中
        if (panel == null)
        {
            // string path = panelPathDict.GetValue(panelType);
            // GameObject panelGo = GameObject.Instantiate(Resources.Load<GameObject>(path), CanvasTransform, false);
            // panel = panelGo.GetComponent<BasePanel>();
            //把资源加载到内存中    
            GameObject prefab = Resources.Load<GameObject>("perfab/shop");
            if (prefab)
            {
                //用内存中GameObject模板克隆一个出来,用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载  
                panel = prefab.GetComponent<BasePanel>();//GameObject.Instantiate(prefab) as BasePanel;
                // if (obj)
                // {
                //     obj.name = "xxxxxxxxxxx";
                // }
            }

            GameObject hp_bar = GameObject.Instantiate(Resources.Load("perfab/shop"));

            //panel = Resources.Load("perfab/shop.prefab") as BasePanel;

            //panel = Resources.Load<BasePanel>("perfab/shop");

            if (!hp_bar)
            {
                Debug.Log("panel is null");
            }


            panelDict.Add(panelType, panel);
        }
        return panel;
    }

    //解析json文件
    private void ParseUIPanelTypeJson()
    {
        // panelPathDict = new Dictionary<string, string>();
        // TextAsset textUIPanelType = Resources.Load<TextAsset>("UIPanelTypeJson");
        // UIPanelInfoList panelInfoList = JsonMapper.ToObject<UIPanelInfoList>(textUIPanelType.text);

        // foreach (UIPanelInfo panelInfo in panelInfoList.panelInfoList)
        // {
        //     panelPathDict.Add(panelInfo.panelType, panelInfo.path);
        //     //Debug.Log(panelInfo.panelType + ":" + panelInfo.path);
        // }
    }
}