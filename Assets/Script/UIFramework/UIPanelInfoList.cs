using System;
using System.Collections.Generic;

[Serializable]
public class UIPanelInfo
{
    public string panelType;
    public string path;

    public UIPanelInfo()
    {

    }
}


[Serializable]
public class UIPanelInfoList
{
    public List<UIPanelInfo> panelInfoList;

    public UIPanelInfoList() { }
}

