using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject holePrefab;
    public float countDelta;
    public int holeCount;
    public int maxHoleAmount = 4;
    void Update()
    {
        // spawning delay
        countDelta -= Time.deltaTime;
        if (countDelta <= 0)
        {
            GenerateRandomSpot();
            countDelta = 0.5f;
        }
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
        if (holeCount < maxHoleAmount)
        {
            SpawnHole(randomPosition);
        }
    }
   
    
    //spawn holes in previously generated position
    private void SpawnHole(Vector3 position)
    {
        Instantiate(holePrefab, position, Quaternion.identity);
        holeCount++;
    }
}

