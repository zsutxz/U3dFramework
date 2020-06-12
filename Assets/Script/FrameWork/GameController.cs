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

    void TestResManager(){

        //float time = System.DateTime.Now;

        for(int i = 1;  i < 1000;  i++){

            GameObject go = null;
            go = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"));  //1
            go = ResManager.Instance.LoadInstance("Prefabs/Cube") as GameObject; //2

            //3、异步加载
            ResManager.Instance.LoadAsync("Prefabs/Cube", (_obj)=>{
                go = _obj as GameObject;
               go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
           });

 
            //4、协程加载
           ResManager.Instance.LoadCoroutine("Prefabs/Cube", (_obj)=>{
                go = _obj as GameObject;
                go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
            });

           go.transform.position = UnityEngine.Random.insideUnitSphere * 20;
       }

 
       //Debug.Log("test:" + (System.Environment.TickCout - time) * 1000);
    }
}