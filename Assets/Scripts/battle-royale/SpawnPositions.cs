using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    public bool SpawnAvailable;
    public float Count;

    private void Start()
    {
        SpawnAvailable = true;
        Count = 1f;
    }

    void Update()
    {
        if (!SpawnAvailable) SpawnTimer();
    }
    
    void SpawnTimer()
    {
        Count -= Time.deltaTime;
        if (Count <= 0)
        {
            SpawnAvailable = true;
            Count = 1f;
        }
    }
}
