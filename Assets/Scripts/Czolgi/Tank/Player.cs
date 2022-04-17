using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {

    public class Player : MonoBehaviour {
        [SerializeField] private float _speed = 3;

        private Rigidbody2D _rgb;
        private Animator _animator;
        private Quaternion _rotation = Quaternion.identity;

        void Start() {
            _rgb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            if (MoveJoystick.direction != Vector2.zero) {
                _rgb.velocity = MoveJoystick.direction * _speed;
                _rotation = Quaternion.LookRotation(Vector3.forward, _rgb.velocity);
                _animator.enabled = true;

            } else {
                _rgb.velocity = Vector2.zero;
                _animator.enabled = false;
            }
            transform.rotation = _rotation;
        }

    }
}