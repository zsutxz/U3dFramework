using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace U3dFramework
{	
	public class EditorUtil
	{
#if UNITY_EDITOR
		[MenuItem("TestCommand/3.MenuItem 复用",false,3)]
		private static void MenuClicked()
		{
			CallMenuItem("TestCommand/2.复制文本到剪切板");
		}

		public static void CallMenuItem(string menuPath)
		{
			EditorApplication.ExecuteMenuItem(menuPath);
		}

		public static void OpenInFolder(string folderPath)
		{
			Application.OpenURL("file:///" + folderPath);
		}

		public static void ExportPackage(string assetPathName, string fileName)
		{
			AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
		}
		
		[UnityEditor.MenuItem("TestCommand/2.复制文本到剪切板", false, -2)]
		private static void MenuClicked2()
		{
			CopyText("要复制的关键字");
		}
#endif

		public static void CopyText(string text)
		{
			GUIUtility.systemCopyBuffer = text;
		}
	}
}
