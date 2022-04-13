using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MysteryBoost : Boost
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Player.BoostPicked)
        {
            boostPicker = UnityEngine.Random.Range(0, 4);
            boostPicker = 3;
            if (boostPicker == 0) collision.SendMessage("AddEffect", EffectType.Freeze);
            if (boostPicker == 1) collision.SendMessage("AddEffect", EffectType.Mass);
            if (boostPicker == 2) collision.SendMessage("AddEffect", EffectType.Speed);
            if (boostPicker == 3) collision.SendMessage("AddEffect", EffectType.Inverse);
            Destroy(gameObject);
            BoostManagerv2.boostCount -= 1;
        }
        
    }
}