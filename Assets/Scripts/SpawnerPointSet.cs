using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPointSet : MonoBehaviour
{
    public static event Action<List<Transform>> OnSpawnPointsSet;

    public List<Transform> currentSpawnPoints;
    public Transform[] allSpawnPoints; // Array of spawn points


    public void SetSpawnPoints(params int[] inputLanes)
    {
        for (int i = 0; i < inputLanes.Length; i++)
        {
            if (inputLanes[i] == 1)
            {

                currentSpawnPoints.Add(allSpawnPoints[i]);
            }
            else
            {
                currentSpawnPoints.Remove(allSpawnPoints[i]);
            }

        }
        OnSpawnPointsSet?.Invoke(currentSpawnPoints);


    }
}
