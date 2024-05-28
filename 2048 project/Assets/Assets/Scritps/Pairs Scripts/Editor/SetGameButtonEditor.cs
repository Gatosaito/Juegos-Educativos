using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetGameButton))]
[CanEditMultipleObjects]
[System.Serializable]
public class SetGameButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetGameButton myScritp = target as SetGameButton;

        switch (myScritp.buttonType)
        {
            case SetGameButton.EButtonType.PairNumberButton:
                myScritp.pairNumber = (PairsGameSettings.EpairNumber)EditorGUILayout.EnumPopup("Pair Numbers", myScritp.pairNumber);
                break;
            case SetGameButton.EButtonType.PuzzleCategoryButton:
                myScritp.puzzleCategories = (PairsGameSettings.EPuzzleCategories)EditorGUILayout.EnumPopup("Puzzle Categories", myScritp.puzzleCategories);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
