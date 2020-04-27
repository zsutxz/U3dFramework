// using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

<<<<<<< HEAD
public class UIManager
=======
namespace MyFrameWork
>>>>>>> 778f75c6193c7e4cda0d23e2b851161a1f0fae66
{
    public class UIManager : MonoBehaviour
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


<<<<<<< HEAD
                return _instance;
=======
                return _instance;
            }
>>>>>>> 778f75c6193c7e4cda0d23e2b851161a1f0fae66
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
<<<<<<< HEAD
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panelStack.Push(panel);
        panel.OnEnter();
    }

=======
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
>>>>>>> 778f75c6193c7e4cda0d23e2b851161a1f0fae66
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


<<<<<<< HEAD
    if (!hp_bar)
=======
            //如果没有实例化面板，寻找路径进行实例化，并且存储到已经实例化好的字典面板中
            if (panel == null)
>>>>>>> 778f75c6193c7e4cda0d23e2b851161a1f0fae66
    {
        GameObject panelGo = GameObject.Instantiate(Resources.Load<GameObject>("prefab/test"), CanvasTransform, false);


<<<<<<< HEAD
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
=======
                panel = panelGo.GetComponent<BasePanel>();
                
                if (!panel)
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
            panelPathDict = new Dictionary<string, string>();
            TextAsset textUIPanelType = Resources.Load<TextAsset>("UIPanelTypeJson");
            //UIPanelInfoList panelInfoList = JsonMapper.ToObject<UIPanelInfoList>(textUIPanelType.text);

            //foreach (UIPanelInfo panelInfo in panelInfoList.panelInfoList)
            {
                panelPathDict.Add("Task", "TaskPanel");
                //Debug.Log(panelInfo.panelType + ":" + panelInfo.path);
            }
        }
>>>>>>> 778f75c6193c7e4cda0d23e2b851161a1f0fae66
}
}