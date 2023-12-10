using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class VectorExtensions 
{
    public static Vector3 TransformToBase(this Vector3 vector, Vector3 forward, Vector3 up, Vector3 right)
    { 
        return new Vector3(
            vector.x * right.x + vector.y * up.x + vector.z * forward.x,
            vector.x * right.y + vector.y * up.y + vector.z * forward.y,
            vector.x * right.z + vector.y * up.z + vector.z * forward.z
        );

    }
}
