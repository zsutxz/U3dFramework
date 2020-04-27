using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using MyFrameWork;

public class TestUI : MonoBehaviour {
    void Start () {
        UIManager panelManager = UIManager.Instance;
        panelManager.PushPanel(UIPanelType.MainMenu);
    }
}
