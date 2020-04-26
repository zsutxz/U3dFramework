using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameobject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectPool<GameObject> gameObjectPool = new ObjectPool<GameObject>(
            (GameObject g)=>
            {
                g.name = "new";
            },
            (GameObject g)=>
            {
                g.name = "release";
            });


        //从池子中获取
        GameObject go = gameObjectPool.Get();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
