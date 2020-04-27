using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using MyFrameWork;

public class TestUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.Shop);
    }
}
