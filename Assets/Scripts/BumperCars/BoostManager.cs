using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static System.Math;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class BoostManager : MonoBehaviour
{
    public GameObject SpeedBoost;
    public GameObject FreezeBoost;
    private int nextUpdate = 1;
    private int Counter = 0;
    private int timeDelta = 3;
    private int BoostsCounter = 0;
    public float Radius = 0.1f * Screen.width;

    protected virtual void Update()
    {
        if(Time.time>=nextUpdate){
            nextUpdate=Mathf.FloorToInt(Time.time)+ timeDelta;
            Boosts();
        }
    }

    Vector2 Spawn()
    {
        float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, Radius)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height - Radius)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(Radius, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - Radius, 0)).x);
            
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        return spawnPosition;
    }

    void Boosts()
    {
        int x = Random.Range(0, 2);
        if (x == 0) Speed();
        else if (x == 1) Freeze();
    }

    void Speed()
    {
        Instantiate(SpeedBoost,  Spawn(), Quaternion.identity);
        BoostsCounter++;
        print(BoostsCounter);
    }

    void Freeze()
    {
        Instantiate(FreezeBoost, Spawn(), Quaternion.identity);
        BoostsCounter++;
        print(BoostsCounter);
    }

}