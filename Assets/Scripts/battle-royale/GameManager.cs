using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<SpawnPositions> spawnPositions;

    // public List<GameObject> SpawnPositions = new List<GameObject>();
    //this is the game manager, the place where we have references to every ManagerObject in this game. It gives us an option to not make reference in every other object
    //simply using reference to GameManager and then to specific objects. It works like a Boss who gives orders to employee 

    private void Awake()
    {
        instance = this;
    }

    //references to game objects
    public List<Player> players;
    public WeaponManager weaponManager;
    
    private void Update()
    {
        weaponManager.SpawnWeapon();
    }
}

