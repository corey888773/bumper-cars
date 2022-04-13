using System;
using System.Collections;
using System.Collections.Generic;
using BumperCars;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // this is the game manager, the place where we have references to every ManagerObject in this game. It gives us an option to not make reference in every other object
    //simply using reference to GameManager and then to specific objects. It works like a Boss who gives orders to employee 
    
    private void Awake()
    {
        instance = this;
    }

    //some variables
    public List<float> holesSpawnTime;
    public bool roundState = true;
   
    //references to game objects
    public List<Player> players;
    public List<Hole> holes;
    public HoleManager holeManager;
    public FloatingTextManager textManager;
    public BoostManagerv2 boostManager;

    private void Update()
    {
        //first round
        Round();

    }

    //floating text method
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration, TextTypes textType, string endOfTimeMassage = "")
    {
        textManager.Show(message, fontSize, color, position, motion, duration, textType, endOfTimeMassage);
    }

    private void Round()
    {
        if (Time.time > holesSpawnTime[0])
        {
            holeManager.SpawnHoles();
            boostManager.SpawnBoost();
        }
        if (Time.time > holesSpawnTime[0] + 10f && roundState)
        {
            for (var i = 0; i < players.Count; i++)
            {
                if (players[i].safe) continue;
                Destroy(players[i].gameObject);
                ShowText("looser", 45,Color.red, players[i].transform.position, Vector3.up * Time.deltaTime, 2f, TextTypes.Text);
                players.RemoveAt(i);
            }

            roundState = false;
        }
    }
}
