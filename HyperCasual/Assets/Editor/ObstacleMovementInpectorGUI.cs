using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObstacleMovement))]
public class ObstacleMovementInpectorGUI : Editor
{
    GameObject targetGameObject;
    Movement_Type movementOn;
    float obstacleSpeed;
    Postion_Movement_Type positionMovement;
    Vector2 movementLimitsOnSelectedAxis;
    Vector3 axesRototationValues;

    public override void OnInspectorGUI()
    {
        ObstacleMovement obstacleMovement = (ObstacleMovement)target;

        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((ObstacleMovement)target), typeof(ObstacleMovement), false);
        GUI.enabled = true;

        EditorGUI.BeginChangeCheck();

        targetGameObject = (GameObject)EditorGUILayout.ObjectField(
            new GUIContent("Target GameObject", "The GameObject that will move."),
            obstacleMovement.targetGameObject,
            typeof(GameObject),
            true);

        movementOn = (Movement_Type)EditorGUILayout.EnumPopup(
            new GUIContent("Movement Type", "Set the GameObject's movement type."),
            obstacleMovement.movementOn);

        switch (obstacleMovement.movementOn)
        {
            case Movement_Type.ON_POSITION:
                OnPositionGUI(obstacleMovement);
                break;

            case Movement_Type.ON_ROTATION:
                OnRotationGUI(obstacleMovement);
                break;

            case Movement_Type.ON_BOTH:
                OnPositionGUI(obstacleMovement);
                OnRotationGUI(obstacleMovement);
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            CheckChanges(obstacleMovement);
        }
    }

    private void CheckChanges(ObstacleMovement obstacleMovement)
    {
        Undo.RecordObject(target, "Changed Area Of Effect");
        obstacleMovement.targetGameObject = targetGameObject;
        obstacleMovement.movementOn = movementOn;
        obstacleMovement.obstacleSpeed = obstacleSpeed;
        obstacleMovement.positionMovement = positionMovement;
        obstacleMovement.movementLimitsOnSelectedAxis = movementLimitsOnSelectedAxis;
        obstacleMovement.axesRototationValues = axesRototationValues;
    }

    private void OnPositionGUI(ObstacleMovement obstacleMovement)
    {
        positionMovement = (Postion_Movement_Type)EditorGUILayout.EnumPopup(
            new GUIContent("Move On", "The position movement axis."),
            obstacleMovement.positionMovement);

        obstacleSpeed = EditorGUILayout.FloatField(
            new GUIContent("Speed of the obstacle", "The GameObject's speed on selected axis."),
            obstacleMovement.obstacleSpeed);

        movementLimitsOnSelectedAxis = EditorGUILayout.Vector2Field(
            new GUIContent("Move Between (Local Position)", "Movement position limits of the object relative to parent object."),
            obstacleMovement.movementLimitsOnSelectedAxis);
    }

    private void OnRotationGUI(ObstacleMovement obstacleMovement)
    {
        axesRototationValues = EditorGUILayout.Vector3Field(
            new GUIContent("Rotation Speed of Axes", "Set X axis Y axis and Z axis rotation values in Degree."),
            obstacleMovement.axesRototationValues);
    }
}