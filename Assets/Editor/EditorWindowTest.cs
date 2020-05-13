using UnityEngine;
using UnityEditor;
using System;

 public class EditorWindowTest : EditorWindow 
{

    [MenuItem("CustomEditorTutorial/WindowTest")]
    private static void ShowWindow() 
    {
        EditorWindowTest myWindow = (EditorWindowTest)EditorWindow.GetWindow(typeof(EditorWindowTest), false, "MyWindow", true);//创建窗口
        myWindow.titleContent = new GUIContent("WindowTest");
        myWindow.Show();
    }

    private void OnGUI() 
    {
        if(GUILayout.Button("Click Me"))
        {
            //Logic
        }
    }
    private void OnFocus() 
    {
        //在2019版本是这个回调
        // SceneView.duringSceneGui -= OnSceneGUI;
        // SceneView.duringSceneGui += OnSceneGUI;

        //以前版本回调
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private void OnDestroy() 
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }
    private void OnSceneGUI( SceneView view ) 
    {
    }
}