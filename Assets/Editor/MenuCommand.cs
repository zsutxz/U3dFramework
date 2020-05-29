using System;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MenuCommand
{
    [MenuItem("TestCommand/SwapGameObject")]
    protected static void SwapGameObject()
    {
        //只有两个物体才能交换
        if( Selection.gameObjects.Length == 2 )
        {
            Vector3 tmpPos = Selection.gameObjects[0].transform.position;
            Selection.gameObjects[0].transform.position = Selection.gameObjects[1].transform.position;
            Selection.gameObjects[1].transform.position = tmpPos;
            //处理两个以上的场景物体可以使用MarkSceneDirty
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty( UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene() );
        }
    }
    
    #if UNITY_EDITOR
    [MenuItem("TestCommand/1.导出 UnityPackage")]
    #endif
    private static void MenuClicked()
    {
        var assetPathName = "Assets/Resources";
        var fileName = GenerateUnityPackageName()+ ".unitypackage";
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
    }


    [MenuItem("TestCommand/2.MenuItem 复用  %e")]
    private static void MenuClicked2()
    {
        EditorApplication.ExecuteMenuItem("TestCommand/1.导出 UnityPackage");
        Application.OpenURL("file:/" + Path.Combine(Application.dataPath, "../"));
    }

	public static string GenerateUnityPackageName()
    {
        return "QFramework_" + DateTime.Now.ToString("yyyyMMdd_hh");
    }

#if UNITY_EDITOR
    [MenuItem("TestCommand/3.屏幕宽高比判断")]
#endif
    private static void MenuResolution()
    {
        Debug.Log(IsPadResolution() ? "是 Pad 分辨率" : "不是 Pad 分辨率");
        Debug.Log(IsPhoneResolution() ? "是 Phone 分辨率" : "不是 Phone 分辨率");
        Debug.Log(IsiPhoneXResolution() ? "是 iPhone X 分辨率" : "不是 iPhone X 分辨率");
    }

    /// <summary>
    /// 获取屏幕宽高比
    /// </summary>
    /// <returns></returns>
    public static float GetAspectRatio()
    {
        return Screen.width > Screen.height ? (float) Screen.width / Screen.height : (float) Screen.height / Screen.width;
    }

    /// <summary>
    /// 是否是 Pad 分辨率 4 : 3 
    /// </summary>
    /// <returns></returns>
    public static bool IsPadResolution()
    {
        var aspect = GetAspectRatio();
        return aspect > 4.0f / 3 - 0.05 && aspect < 4.0f / 3 + 0.05;
    }
    
    /// <summary>
    /// 是否是手机分辨率 16:9
    /// </summary>
    /// <returns></returns>
    public static bool IsPhoneResolution()
    {
        var aspect = GetAspectRatio();
        return aspect > 16.0f / 9 - 0.05 && aspect < 16.0f / 9 + 0.05;
    }
    
    /// <summary>
    /// 是否是iPhone X 分辨率 2436:1125
    /// </summary>
    /// <returns></returns>
    public static bool IsiPhoneXResolution()
    {
        var aspect = GetAspectRatio();
        return aspect > 2436.0f / 1125 - 0.05 && aspect < 2436.0f / 1125 + 0.05;
    }

    #if UNITY_EDITOR
    [MenuItem("TestCommand/4.Transform 赋值优化",false ,-1)]
    [Obsolete("Test 方法已过时，请使用 XXX;")]
    #endif
    private static void SetLocalPos()
    {
        var transform = new GameObject("transform").transform;

        SetLocalPosX(transform, 5.0f);
    }

    public static void SetLocalPosX(Transform transform, float x)
    {
        Vector3 localPos = transform.localPosition;
        localPos.x = x;
        float temp_x = transform.localPosition.x;
        transform.localPosition = localPos;
    }


    public partial class MonoBehaviourSimplify:MonoBehaviour 
    {
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }
        
        private static IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);

            onFinished();
        }
    }

    public class DelayWithCoroutine : MonoBehaviourSimplify
    {
        private void Start()
        {
            Delay(5.0f, () =>
            {
                UnityEditor.EditorApplication.isPlaying = false;
            });
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("TestCommand/6.定时功能", false, 11)]
        private static void MenuClickd()
        {
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("DelayWithCoroutine").AddComponent<DelayWithCoroutine>();
        }
#endif
    }

}
