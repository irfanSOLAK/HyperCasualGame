using UnityEngine;

public enum Game_Events { Countdown, StartRunning, FinishRunning, PaintWall, };

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
    ON_X_AXIS,

    [InspectorName("Y Axis")]
    ON_Y_AXIS,

    [InspectorName("Z Axis")]
    ON_Z_AXIS,
}