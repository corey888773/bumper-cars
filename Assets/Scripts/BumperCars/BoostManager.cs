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
    public GameObject speedBoost;
    public GameObject freezeBoost;
    public GameObject massBoost;
    private int _nextUpdate = 1;
    private int timeDelta = 5;
    protected static int BoostsCounter = 0;
    public float radius = 0.1f * Screen.width;

    protected virtual void Update()
    {
        if(Time.time>=_nextUpdate){
            _nextUpdate=Mathf.FloorToInt(Time.time)+ timeDelta;
            if (BoostsCounter <= 2) Boosts();
            print(Player._velocity);
        }
    }

    Vector2 Spawn()
    {
        float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, radius)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height - radius)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(radius, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - radius, 0)).x);
            
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        return spawnPosition;
    }

    void Boosts()
    {
        int x = Random.Range(0, 3);
        if (x == 0) Speed();
        else if (x == 1) Freeze();
        else if (x == 2) Mass();
    }

    void Mass()
    {
        Instantiate(massBoost, Spawn(), Quaternion.identity);
        BoostsCounter++;
    }

    void Speed()
    {
        Instantiate(speedBoost,  Spawn(), Quaternion.identity);
        BoostsCounter++;
    }

    void Freeze()
    {
        Instantiate(freezeBoost, Spawn(), Quaternion.identity);
        BoostsCounter++;
    }

}