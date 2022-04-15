using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBoost : Boost
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerUnsafe") || !collision.CompareTag("PlayerSafe"))
            return;
        if (!Player.BoostPicked)
        {
            collision.SendMessage("AddEffect", EffectType.Mass);
            Destroy(gameObject);
            BoostManagerv2.boostCount -= 1;
        }
        
    }
}