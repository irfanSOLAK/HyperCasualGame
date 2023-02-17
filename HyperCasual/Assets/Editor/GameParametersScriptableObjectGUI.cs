using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameParametersScriptableObject))]
public class GameParametersScriptableObjectGUI : Editor
{
    bool isMainMenuParametersShowing;
    bool isCameraParametersShowing;
    bool isGameStarterParametersShowing;
    bool isCharacterMovementParametersShowing;
    bool isPaintParametersShowing;
    bool isRankingParametersShowing;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameParametersScriptableObject gameParametersScriptableObject = (GameParametersScriptableObject)target;

        OnCameraParametersGUI();
        OnMainMenuParametersGUI();
        OnGameStarterParametersGUI();
        OnCharacterMovementParametersGUI();
        OnPaintParametersGUI();
        OnRankingParametersGUI();

        serializedObject.ApplyModifiedProperties();
    }
    private void OnRankingParametersGUI()
    {
        isRankingParametersShowing = EditorGUILayout.Foldout(isRankingParametersShowing, "RANKING PARAMETERS");

        if (isRankingParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("runningTextColor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("runningCompletedTextColor"));
        }

        EditorGUILayout.Space(10);
    }

    private void OnPaintParametersGUI()
    {
        isPaintParametersShowing = EditorGUILayout.Foldout(isPaintParametersShowing, "PAINT PARAMETERS");

        if (isPaintParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("brushPainted"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("brushUnpainted"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("brushCursorSize"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("paintingSize"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("paintingColor"));
        }

        EditorGUILayout.Space(10);
    }

    private void OnCharacterMovementParametersGUI()
    {
        isCharacterMovementParametersShowing = EditorGUILayout.Foldout(isCharacterMovementParametersShowing, "CHARACTER MOVEMENT PARAMETERS");

        if (isCharacterMovementParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterForwardSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterHorizontalSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterXAxisDisplacement"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterXAxisLimits"));
        }

        EditorGUILayout.Space(10);
    }

    private void OnGameStarterParametersGUI()
    {
        isGameStarterParametersShowing = EditorGUILayout.Foldout(isGameStarterParametersShowing, "GAME STARTER PARAMETERS");

        if (isGameStarterParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("countdownInSeconds"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("runningGameplayInfo"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("paintingGameplayInfo"));
        }

        EditorGUILayout.Space(10);
    }

    private void OnMainMenuParametersGUI()
    {
        isMainMenuParametersShowing = EditorGUILayout.Foldout(isMainMenuParametersShowing, "MAIN MENU PARAMETERS");

        if (isMainMenuParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("game1Info"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("game2Info"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("game3Info"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultText"));
        }

        EditorGUILayout.Space(10);
    }

    private void OnCameraParametersGUI()
    {
        isCameraParametersShowing = EditorGUILayout.Foldout(isCameraParametersShowing, "CAMERA PARAMETERS");

        if (isCameraParametersShowing)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startingPosition"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraToTargetDistance"));
        }

        EditorGUILayout.Space(10);
    }
}