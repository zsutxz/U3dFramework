using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using U3dFramework;

public class GameController : MonoSingleton<GameController>{

    //Use this for initialization
    void Start(){
        ModuleManager.Instance.RegisterAllModules();
        SceneManager.Instance.RegisterAllScene();
    }

    private IEnumerator AutoUpdateGold(){

        int gold = 0;

        while(true){

            gold++;
            yield return new WaitForSeconds(1.0f);

            //Message message = new Message(MessageType.Net_MessageTestOne.ToString(), this);
            //message[“gold”] = gold;
            //MessageCenter.Instance.SendMessage(message);
        }
    }

   private IEnumerator<int> AsyncLoadData(){

        int i = 0;

        while(true){

            Debug.Log("test" + i);
            //Debug.Break();

            yield return i;
            i++;
        }
    }
}