using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All Extension Methods are described here.
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Changes transform's X axis position only to given float value using localPosition property.
    /// </summary>
    public static Vector3 ChangeLocalPositionX(this Transform transform, float x)
    {
        Vector3 position = transform.localPosition;
        position.x = x;
        transform.localPosition = position;
        return position;
    }


    /// <summary>
    /// Changes transform's Y axis position only to given float value using localPosition property.
    /// </summary>
    public static Vector3 ChangeLocalPositionY(this Transform transform, float y)
    {
        Vector3 position = transform.localPosition;
        position.y = y;
        transform.localPosition = position;
        return position;
    }


    /// <summary>
    /// Changes transform's Z axis position only to given float value using localPosition property.
    /// </summary>
    public static Vector3 ChangeLocalPositionZ(this Transform transform, float z)
    {
        Vector3 position = transform.localPosition;
        position.z = z;
        transform.localPosition = position;
        return position;
    }
}
