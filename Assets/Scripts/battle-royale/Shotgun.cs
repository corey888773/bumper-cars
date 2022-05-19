using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour
{
    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVelocity = 1;

    public GameObject pelletPrefab;
    public GameObject flamePrefab;
    public Player _parent;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    public Transform shotgunExit;

    private List<Quaternion> pellets;   
    // Start is called before the first frame update
    private void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }

        Activate(false);
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Input.GetButtonDown("Jump") && _parent.WeaponIsPicked() == WeaponType.Shotgun)
        {
            Fire();
            _parent.ThrowWeapon(WeaponType.Shotgun);
            // _animator.SetTrigger("Fire");
        }
    }
    private void Fire()
    {
        for(int i = 0; i < pellets.Count; i++)
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pelletPrefab, shotgunExit.position, shotgunExit.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * pelletFireVelocity * Random.Range(5, 10) * 0.1f);
        }

        Instantiate(flamePrefab, _parent.gameObject.transform.position, quaternion.identity);
        _parent.KnockBack(transform.up * pelletFireVelocity);
        _animator.SetTrigger("Fire");
        
    }
    public void Activate(bool state)
         {
             if(state)
                _animator.SetTrigger("Get");
             _spriteRenderer.enabled = state;
         }
}

