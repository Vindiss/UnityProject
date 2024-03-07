using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{
    public static List<Vector3> GeneratePoints(float minDistance, Vector3 regionSize, int testBeforeNext)
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> spawnPoints = new List<Vector3>();

        spawnPoints.Add(regionSize / 2);

        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector3 spawnCentre = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for (int i = 0; i < testBeforeNext; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                Vector3 candidate = spawnCentre + dir * Random.Range(minDistance, 2 * minDistance);

                if (IsValid(candidate, regionSize, minDistance, points))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    candidateAccepted = true;
                    break;
                }
            }

            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }

        return points;
    }

    static bool IsValid(Vector3 candidate, Vector3 regionSize, float minDistance, List<Vector3> points)
    {
        if (candidate.x >= 0 && candidate.x < regionSize.x && candidate.z >= 0 && candidate.z < regionSize.z)
        {
            foreach (Vector3 point in points)
            {
                if (Vector3.Distance(candidate, point) < minDistance)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
