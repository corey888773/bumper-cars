using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Boost
{
    protected void OnTriggerEnter2D(Collider2D colision)
    {
        colision.SendMessage("AddEffect", EffectType.Speed);
    }
}
