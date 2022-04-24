using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    private Vector2 _defaultPositon;
    private Camera cam_;
    private bool pressActivated;
    private Vector2 currentTargetPosition_;

    private void Awake()
    {
        cam_ = GameObject.Find("Main Camera").GetComponent<Camera>();
        _defaultPositon = transform.position;
    }

    private void Update()
    {
        int best_index = -1;
        float best_dist_sqr = 100000f;
        Vector2 best_pos = new Vector2(0.0f, 0.0f);

       if(Input.touchCount > 0)
        {
            if(!pressActivated)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 pos = ((Vector2) cam_.ScreenToWorldPoint(touch.position));
                transform.position = pos;
                
                
                pressActivated = true;
            }

        }
       else
       {
           transform.position = _defaultPositon;
           pressActivated = false;
       }
       
    }
}
