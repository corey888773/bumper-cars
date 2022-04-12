using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManagerv2 : MonoBehaviour
{
    public List<GameObject> boostPrefab;
    public float Count;
    public int boostCount = 0;
    public int maxBoostAmount = 1;
    void Update()
    {
        
    }
    public void GenerateRandomSpot()
    {
        //generates a cords in Camera range
        float ranY = Random.Range
        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0.1f * Screen.height)).y, 
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.9f)).y);
        float ranX = Random.Range
        (Camera.main.ScreenToWorldPoint(new Vector2(0.1f * Screen.width, 0)).x, 
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.9f, 0)).x);

        Vector3 randomPosition = new Vector3(ranX, ranY, 0f);
        
        //specifies the amount of holes
        if (boostCount < maxBoostAmount)
        {
            InstantiateHole(randomPosition);
        }
    }
   
    
    //spawn holes in previously generated position
    private void InstantiateHole(Vector3 position)
    {
        Instantiate(boostPrefab[0], position, Quaternion.identity);
        boostCount++;
    }

    
    public void SpawnBoost()
    {
        // spawning delay
        Count -= Time.deltaTime; 
        if (Count <= 0)
        {
            GenerateRandomSpot();
            Count = 3f;
        }
    }
}
