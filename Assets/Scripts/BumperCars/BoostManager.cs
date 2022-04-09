using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using BumperCars;
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
    private int timeDelta = 6;
    protected static int BoostsCounter = 0;
    public float radius = 0.1f * Screen.width;

    protected virtual void Update()
    {
        if(Time.time>=_nextUpdate){
            _nextUpdate=Mathf.FloorToInt(Time.time)+ timeDelta;
            if (BoostsCounter <= 2) Boosts();
        }

        if (SpeedManager.boosting)
        {
            SpeedManager.boostTime += Time.deltaTime;
            if (SpeedManager.boostTime >= 20)
            {
                Player._velocity = Player.StartingVelocity;
                SpeedManager.boostTime = 0f;
                SpeedManager.boosting = false;
            }
        }
        if (MassManager.boosting)
        {
            MassManager.boostTime += Time.deltaTime;
            if (MassManager.boostTime >= 20)
            {
                GameObject.Find("Player").GetComponent<Rigidbody2D>().mass = Player.StartingMass;
                MassManager.boostTime = 0f;
                MassManager.boosting = false;
            }
        }
        if (FreezeManager.boosting)
        {
            FreezeManager.boostTime += Time.deltaTime;
            if (FreezeManager.boostTime >= 20)
            {
                GameObject.Find("Player").GetComponent<Rigidbody2D>().mass = Player.StartingMass;
                Player._velocity = Player.StartingVelocity;
                FreezeManager.boostTime = 0f;
                FreezeManager.boosting = false;
            }
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
        int x = Random.Range(0, 7);
        if (x == 0) Freeze();
        else if (x % 2 == 0) Speed();
        else if (x % 2 == 1) Mass();
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