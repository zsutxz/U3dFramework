using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameStateCtrl gs = new GameStateCtrl();
        gs.Initialize();
        gs.GotoState("LoginState");
        gs.GotoState("MainCityState");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
