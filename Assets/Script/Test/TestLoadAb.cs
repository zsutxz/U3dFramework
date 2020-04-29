using UnityEngine;
using System.Collections;
using System;

public class TestLoadNewAB : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("LoadAssetbundle"))
        {
            DateTime t0 = DateTime.Now;
            //首先加载Manifest文件;
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath
                                                                  + "/Android/Android");

            Debug.Log(manifestBundle == null);
            if (manifestBundle != null)
            {
                AssetBundleManifest manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
                Debug.Log(manifest == null);
                //获取依赖文件列表;
                string[] cubedepends = manifest.GetAllDependencies("cube.data");

                Debug.Log(cubedepends.Length);
                foreach (var item in cubedepends)
                {
                    Debug.Log("depends " + item);
                }
                AssetBundle[] dependsAssetbundle = new AssetBundle[cubedepends.Length];

                for (int index = 0; index < cubedepends.Length; index++)
                {
                    //加载所有的依赖文件;
                    dependsAssetbundle[index] = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Android/" + cubedepends[index]);

                }
                DateTime t1 = DateTime.Now;

                //加载我们需要的文件;"
                AssetBundle cubeBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Android/cube.data");
                Debug.Log(cubeBundle == null);
                GameObject cube = cubeBundle.LoadAsset("cube") as GameObject;
                Debug.Log(cube == null);
                if (cube != null)
                {
                    Instantiate(cube);


                    DateTime t2 = DateTime.Now;

                    Debug.Log("LoadView Time1:" + (t1 - t0).TotalMilliseconds + " Time2:" + (t2 - t1).TotalMilliseconds);//+ " Time3:" + (t3 - t2).TotalMilliseconds
                }
            }


        }
    }
}