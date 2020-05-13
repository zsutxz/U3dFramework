using UnityEditor;
using UnityEngine;

public class TestScriptWizard: ScriptableWizard 
{

    [MenuItem("CustomEditorTutorial/TestScriptWizard")]
    private static void MenuEntryCall() 
    {
        ScriptableWizard.DisplayWizard("Copy an object.",typeof(TestScriptWizard),"Copy!");
    }

    private void OnWizardCreate() 
    {

    }
}
