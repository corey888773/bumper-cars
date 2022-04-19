using System;
using System.Collections;
using System.Collections.Generic;
using BumperCars;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //this is the game manager, the place where we have references to every ManagerObject in this game. It gives us an option to not make reference in every other object
    //simply using reference to GameManager and then to specific objects. It works like a Boss who gives orders to employee 

    private void Awake()
    {
        instance = this;
    }

    //some variables
    private bool round;
    private bool spawn;
    private bool start;
    private float lastRound;
    

    //references to game objects
    public List<Player> players;
    public HoleManager holeManager;
    public FloatingTextManager textManager;
    public BoostManagerv2 boostManager;

    private void Start()
    {
        lastRound = -3f;
    }
    private void Update()
    {
        boostManager.SpawnBoost();
        
        lastRound += Time.deltaTime;
        if (lastRound > 0f && !round)
        {
            round =  holeManager.SpawnHoles();
            start = true;
            
        }
        //first round
        Round();
    }

    //floating text method
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration,
        TextTypes textType, string endOfTimeMassage = "")
    {
        textManager.Show(message, fontSize, color, position, motion, duration, textType, endOfTimeMassage);
    }

    private void Round()
    {
        //two conditions, they have to be separated
        if(!round) return;
        if (holeManager.holeCount != 0 || !start) return;
        
        //for every player, check if he's safe, and if not Destroy him.
        for (var i = 0; i < players.Count; i++)
        {
            if (players[i].safe)
            {
                players[i].safe = false;
                continue;
            }
            // Destroy(players[i].gameObject);
            players[i].gameObject.SetActive(false);
            ShowText("looser", 45, Color.red, players[i].transform.position, Vector3.up * Time.deltaTime, 2f, TextTypes.Text);
            players.RemoveAt(i);
        }
        start = false;
        round = false;
        // sets the delay between rounds
        lastRound = -9f;
    }
}

