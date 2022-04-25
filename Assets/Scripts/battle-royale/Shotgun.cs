using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Shotgun : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        _weaponType = WeaponType.Shotgun;
    }
}