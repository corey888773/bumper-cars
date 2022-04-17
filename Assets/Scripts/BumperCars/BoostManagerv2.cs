using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManagerv2 : MonoBehaviour
{
    public List<GameObject> boostPrefab;
    public float Count;
    public static int boostCount;
    public int maxBoostAmount = 2;
    
    void Start()
    {
        boostCount = 0;
        boostPrefab = new List<GameObject>(Resources.LoadAll<GameObject>("bumper-cars"));
    }
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
            InstantiateBoost(randomPosition);
        }
    }
   
    
    //spawn boosts in previously generated position
    private void InstantiateBoost(Vector3 position)
    {
        var boostPicker = Random.Range(0, 3);
        Instantiate(boostPrefab[1], position, Quaternion.identity);
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
