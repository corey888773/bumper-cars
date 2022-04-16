using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {

    public class Player : MonoBehaviour {
        [SerializeField] private float speed = 10;
        private Rigidbody2D rgb;
        private Quaternion _rotation = new Quaternion(0, 0, 0, 0);
        void Start() {
            rgb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (MoveJoystick.direction != Vector2.zero) {
                rgb.velocity = MoveJoystick.direction * speed;
                _rotation = Quaternion.LookRotation(Vector3.forward, rgb.velocity);

            } else {
                rgb.velocity = Vector2.zero;
            }
            transform.rotation = _rotation;
        }

    }
}