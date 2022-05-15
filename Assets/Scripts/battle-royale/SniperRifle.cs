using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SniperRifle : MonoBehaviour
{
    
    public float bulletFireVelocity = 1;

    public GameObject bulletPrefab;
    public Player _parent;
    public LineRenderer _laser;
    public float laserDistance = 100f;
    
    
    public Transform riffleExit;

    private Transform laserTransform;
    
    private void Awake()
    {
        laserTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetButtonDown("Jump") && _parent.WeaponIsPicked())
        {
            
            Fire();
            _parent.ThrowWeapon(WeaponType.SniperRifle);
        }
        ShootLaser();
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, riffleExit.position, riffleExit.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletFireVelocity);

        _parent.KnockBack(transform.up * bulletFireVelocity,true);
    }

    private void ShootLaser()
    {
        if (Physics2D.Raycast(laserTransform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(riffleExit.position, transform.up, Mathf.Infinity,
                LayerMask.GetMask("Walls"));
            Draw2DRay(riffleExit.position, _hit.point);
            
        }
        else
        {
            Draw2DRay(riffleExit.position, riffleExit.transform.up * laserDistance);
        }
    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        _laser.SetPosition(0, startPos);
        _laser.SetPosition(1, endPos);
    }
    
}
