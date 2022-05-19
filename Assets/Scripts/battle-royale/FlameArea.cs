using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlameArea : MonoBehaviour
{
    public float duration = 3f;
    public float activation = 0.1f;
    private float instaniateTime;
    private SpriteRenderer _spriteRenderer;
    private float startX;
    private float value = 1/5f;
    private float scale = 2.5f;
    private readonly Vector3 degreeVector3 = new Vector3(0, 0 ,360);

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        instaniateTime = Time.time;
        startX = 0.5f;
    }
    private void Update()
    {
        startX += Time.deltaTime * value * Random.Range(0, 1);
        if (startX is < 0.4f or > 0.6f)
            value *= -1;

        var X = Math.Sin(startX);
        var Y = 1 - X;
        
        if(scale > 1f)
            scale -= Time.deltaTime;
        
        transform.localScale = new Vector3((float)X * scale, (float)Y * scale, 0);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, degreeVector3, Time.deltaTime);
        
        if (Time.time - instaniateTime > activation)
            _spriteRenderer.enabled = true;
            
        if (Time.time - instaniateTime > duration)
            Destroy(gameObject);
      
    }
}
