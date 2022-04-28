using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private float spawnTime;
    private bool confirmed;
    private WeaponManager _weaponManager;
    private Collider2D _checkCollider;
    private SpriteRenderer _spriteRenderer;
    protected int weaponPicker;
    protected WeaponType _weaponType = WeaponType.Gun;

    protected virtual void Awake()
    {
        _weaponManager = FindObjectOfType<WeaponManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        spawnTime = Time.time;
        _weaponManager.weapons.Add(this);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // if (!collision.CompareTag("Player")) return;
        // foreach (var player in GameManager.instance.players.Where(player => player._boxCollider2D == collision && !player.WeaponPicked))
        // {
        //     collision.SendMessage("AddEffect", _weaponType);
        //     Destroy(gameObject);
        //     WeaponManager.weaponCount -= 1;
        // }
        if (!Player.WeaponPicked)
        {
            collision.SendMessage("AddEffect", _weaponType);
            Destroy(gameObject);
            WeaponManager.weaponCount -= 1;
        }
    }

    protected void Update()
    {
        // confirm hole after 1 second of existence
        if (Time.time - spawnTime > 1f && !confirmed)
            ConfirmBoost();
    }
    private void ConfirmBoost()
    {
        {
            // gameObject.layer = 7;
            // _spriteRenderer.enabled = true;
            confirmed = true;
            
        } 
    }

    ~Weapon()
    {
        _weaponManager.weapons.Remove(this);
    }
}