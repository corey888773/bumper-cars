using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Czolgi
{
    public class MoveJoystick : MonoBehaviour
    {
        public float speed;
        public Transform pad;
        private Vector3 padStartPosition, newPosition;
        private Vector3 position, screenPoint;
        private Camera joystickCamera;
        private float maxTouchDifferene;
        Resolution res;
        private Vector2 _joystickPos;

        public static float horizontal, vertical;
        private void Awake()
        {
            _joystickPos = transform.position;
            _joystickPos = Camera.main.WorldToScreenPoint(_joystickPos);
            padStartPosition = pad.localPosition;
            res = Screen.currentResolution;
            joystickCamera = FindObjectOfType<Camera>();
            screenPoint = joystickCamera.WorldToScreenPoint(transform.position);
            maxTouchDifferene = joystickCamera.WorldToScreenPoint(transform.position + new Vector3(1.5f, 0, 0)).x - screenPoint.x;

        }


        void Update()
        {
            if (res.width != Screen.width || res.height != Screen.height)
            {
                screenPoint = joystickCamera.WorldToScreenPoint(transform.position);
                res = Screen.currentResolution;
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && Vector2.Distance(_joystickPos, touch.position) < maxTouchDifferene * 2)
                // distance statement checks if touch is inside the circle of 350px
                {
                    var xDifference = touch.position.x - screenPoint.x;
                    horizontal = xDifference / maxTouchDifferene;
                    if (horizontal > 1) horizontal = 1;
                    if (horizontal < -1) horizontal = -1;

                    var yDifference = touch.position.y - screenPoint.y;
                    vertical = yDifference / maxTouchDifferene;
                    if (vertical > 1) vertical = 1;
                    if (vertical < -1) vertical = -1;
                    newPosition = padStartPosition + new Vector3(1.5f * horizontal, 1.5f * vertical, 0).normalized;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    horizontal = 0;
                    vertical = 0;
                    newPosition = padStartPosition + new Vector3(1.5f * horizontal, 1.5f * vertical, 0);
                }
            }
            pad.localPosition = Vector3.Lerp(pad.localPosition, newPosition, speed * Time.deltaTime);
        }
    }
}


