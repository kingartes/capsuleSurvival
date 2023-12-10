using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorUtils : MonoBehaviour
{
    public static List<Vector3> GetVectorsInCone(Vector3 direction, float coneWidthDegree, int divisionsCount)
    {
        List<Vector3> coneVectors = new List<Vector3>();
        Vector3 leftBound = Quaternion.Euler(0, -coneWidthDegree/2, 0f) * direction;
        Vector3 rightBound = Quaternion.Euler(0, coneWidthDegree / 2, 0f) * direction;
        Vector3 scanDirection = leftBound;
        float scanIterationDegree = Vector3.Angle(leftBound, rightBound) / divisionsCount;
        for (int i = 0; i < divisionsCount; i++)
        {
            scanDirection = Quaternion.Euler(0f, scanIterationDegree, 0f) * scanDirection;
            coneVectors.Add(scanDirection.normalized);
        }
        return coneVectors;
    }
}
