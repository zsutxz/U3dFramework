using UnityEditor;
using UnityEngine;
using System;
using System.IO;

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
    
    [MenuItem("TestCommand/1.导出 UnityPackage")]
    private static void MenuClicked()
    {
        var assetPathName = "Assets/Resources";
        var fileName = "QFramework_" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
    }


    [MenuItem("TestCommand/2.MenuItem 复用  %e")]
    private static void MenuClicked2()
    {
        EditorApplication.ExecuteMenuItem("TestCommand/1.导出 UnityPackage");
        Application.OpenURL("file:/" + Path.Combine(Application.dataPath, "../"));
    }


}
