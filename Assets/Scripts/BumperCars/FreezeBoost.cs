using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FreezeBoost : Boost
{
    protected override void OnTriggerEnter2D(Collider2D colision)
    {
        if (!Player.BoostPicked)
        {
            colision.SendMessage("AddEffect", EffectType.Freeze);
            Destroy(gameObject);
            BoostManagerv2.boostCount -= 1;
        }
    }
}