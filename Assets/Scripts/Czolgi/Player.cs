using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Rigidbody2D rgb;
        void Start()
        {
            rgb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {


            rgb.velocity = new Vector2(MoveJoystick.horizontal, MoveJoystick.vertical).normalized * speed;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, rgb.velocity);
            transform.rotation = rotation;
        }



    }
}