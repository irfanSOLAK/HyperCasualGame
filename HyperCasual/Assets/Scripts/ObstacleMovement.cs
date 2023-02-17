using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public enum Movement_Type
    {
        [InspectorName("Position")]
        ON_POSITION,

        [InspectorName("Rotation")]
        ON_ROTATION,

        [InspectorName("Position and Rotation")]
        ON_BOTH,
    }

    public enum Postion_Movement_Type
    {
        [InspectorName("X Axis")]
        ON_X_AXÝS,

        [InspectorName("Y Axis")]
        ON_Y_AXÝS,

        [InspectorName("Z Axis")]
        ON_Z_AXÝS,
    }

    public GameObject targetGameObject;
    public Movement_Type movementOn;
    public Postion_Movement_Type positionMovement;
    public float obstacleSpeed;
    public Vector2 movementLimitsOnSelectedAxis;
    public Vector3 axesRototationValues;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        switch (movementOn)
        {
            case Movement_Type.ON_POSITION:
                MoveBetweenPositionLimits();
                break;

            case Movement_Type.ON_ROTATION:
                RotateObject();
                break;

            case Movement_Type.ON_BOTH:
                RotateObject();
                MoveBetweenPositionLimits();
                break;
        }
    }

    private void MoveBetweenPositionLimits()
    {
        switch (positionMovement)
        {
            case Postion_Movement_Type.ON_X_AXÝS:
                targetGameObject.transform.ChangeLocalPositionX(Mathf.Lerp(
                    movementLimitsOnSelectedAxis.x,
                    movementLimitsOnSelectedAxis.y,
                    Mathf.PingPong(Time.time * obstacleSpeed, 1.0f)));
                break;

            case Postion_Movement_Type.ON_Y_AXÝS:
                targetGameObject.transform.ChangeLocalPositionY(Mathf.Lerp(
                    movementLimitsOnSelectedAxis.x,
                    movementLimitsOnSelectedAxis.y,
                    Mathf.PingPong(Time.time * obstacleSpeed, 1.0f)));
                break;

            case Postion_Movement_Type.ON_Z_AXÝS:
                targetGameObject.transform.ChangeLocalPositionZ(Mathf.Lerp(
                    movementLimitsOnSelectedAxis.x,
                    movementLimitsOnSelectedAxis.y,
                    Mathf.PingPong(Time.time * obstacleSpeed, 1.0f)));
                break;
        }
    }

    private void RotateObject()
    {
        targetGameObject.transform.rotation *= Quaternion.Euler(axesRototationValues.x, axesRototationValues.y, axesRototationValues.z);
    }
}