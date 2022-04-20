using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FreezeBoost : Boost
{
    protected override void Awake()
    {
        base.Awake();
        _effectType = EffectType.Freeze;
    }
}