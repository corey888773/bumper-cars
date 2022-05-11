using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : Weapon
{
    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVelocity = 1;

    public GameObject pelletPrefab;

    public Transform shotgunExit;

    private List<Quaternion> pellets;   
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _weaponType = WeaponType.Shotgun;
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Input.GetButtonDown("Jump") && Player.shotgunPicked)
        {
            Fire();
        }
    }

    private void Fire()
    {
        for(int i = 0; i < pellets.Count; i++)
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pelletPrefab, shotgunExit.position, shotgunExit.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * pelletFireVelocity);
        }
    }
    
}

