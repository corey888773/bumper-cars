using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static System.Math;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class HolesManager : MonoBehaviour
{
    public GameObject hole;
    // public bool is_hidden;
    private int nextUpdate = 1;
    public int timeDelta = 3;
    
    void Start()
    {
        
    }
    
    void Update(){
    
        // If the next update is reached
        if(Time.time>=nextUpdate){
            nextUpdate=Mathf.FloorToInt(Time.time)+ timeDelta;
            UpdateEverySecond();
        }
     
    }

    void Spawn()
    {
        // float spawnY = Player._rigidbody2D.position.y;
        // float spawnX = Player._rigidbody2D.position.x;
        // while (spawnY < Player._rigidbody2D.position.y + Screen.height * 0.01 ||
        //        spawnX < Player._rigidbody2D.position.x + Screen.width * 0.01)
        // {
        //     spawnY = Random.Range
        //         (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        //     spawnX = Random.Range
        //         (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        // }
        // Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        // Instantiate(hole, spawnPosition, Quaternion.identity);
        
        for (int i = 0; i < 3; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0.1f * Screen.height)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.9f)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0.1f * Screen.width, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.9f, 0)).x);
            
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            
            Destroy(Instantiate(hole, spawnPosition, Quaternion.identity), 2.0f);
        }
    }

    void Delete()
    {
        
    }
     
    // Update is called once per second
    void UpdateEverySecond()
    {
        Delete();
        Spawn();
    }
}
