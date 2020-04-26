using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnPause();
    public abstract void OnResume();
    public abstract void OnExit();
}

