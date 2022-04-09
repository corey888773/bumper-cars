using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;
using System;
using Random = System.Random;

public class ComputerPlayer : Player
{
    public float lastUpdate = 0;
    public float cooldown = 0.2f;
    public float force = 100.0f;
    private Vector2 _randomForceVector;
    public GameObject player;

    private void RandomForce()
    {
        if (Time.time - lastUpdate > cooldown)
        {
            _randomForceVector = new Vector2(player.transform.position.x - transform.position.x,
                player.transform.position.y - transform.position.y) * force;
            
            Move(_randomForceVector);

        }
    }
    protected override void FixedUpdate()
    {
        RandomForce();
    }
}


