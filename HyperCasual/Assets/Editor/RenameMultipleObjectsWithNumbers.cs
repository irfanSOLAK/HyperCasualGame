using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RenameMultipleObjectsWithNumbers : ScriptableWizard
{
    //Base name
    public string BaseName = "MyObject_";
    //Start Count
    public int StartNumber = 0;
    //Increment
    public int Increment = 1;

    [MenuItem("GameObject/My Settings/Rename Multiple Objects With Numbers #R", priority = 20)] // % shortcut for ctrl # shortcut for shift
    static void CreateWizard()
    {
        if (ScriptableWizard.GetWindow<RenameMultipleObjectsWithNumbers>() == null)
        {
            ScriptableWizard.DisplayWizard("Rename Multiple Objects With Numbers", typeof(RenameMultipleObjectsWithNumbers), "Rename");
        }
    }
    //Called when the window first appears
    void OnEnable()
    {
        UpdateSelectionHelper();
    }
    //Function called when selection changes in scene
    void OnSelectionChange()
    {
        UpdateSelectionHelper();
    }
    //Update selection counter
    void UpdateSelectionHelper()
    {
        helpString = "";
        if (Selection.objects != null)
            helpString = "Number of objects selected: " + Selection.objects.Length;
    }
    //Rename
    void OnWizardCreate()
    {
        //If selection empty, then exit
        if (Selection.objects == null)
            return;
        //Current Increment
        int PostFix = StartNumber;
        //Cycle and rename
        foreach (Object O in Selection.objects)
        {
            O.name = BaseName + PostFix;
            PostFix += Increment;
        }
    }
}
