using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    
    // this is the game manager, the place where we have references to every object in this game. It gives us an option to not make reference in every other object
    //simply using reference to GameManager and then to specific objects. It works like a Boss who gives orders to employee 
    
    private void Awake()
    {
        instance = this;
    }

    //some variables
    public float firstHolesSpawn = 3f;
    
    //references to game objects
    public List<Player> players;
    public List<Hole> holes;
    public HoleManager holeManager;
    public FloatingTextManager textManager;

    private void Update()
    {
        if (Time.time > firstHolesSpawn)
            holeManager.SpawnHoles();
    }

    //floating text method
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        textManager.Show(message, fontSize, color, position, motion, duration);
    }
}
