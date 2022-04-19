using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {

    public class Player : MonoBehaviour {
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _MaxHP = 5;
        [SerializeField] private float _HP = 5;

        private Rigidbody2D _rgb;
        private Animator _animator;
        private Quaternion _rotation = Quaternion.identity;

        void Start() {
            _rgb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            Events.OnPlayerGetDemage.Invoke(_HP / _MaxHP);
        }

        private void FixedUpdate() {
            if (MoveJoystick.direction == Vector2.zero) {
                _rgb.velocity = Vector2.zero;
                _animator.enabled = false;
                return;
            }

            _rgb.velocity = MoveJoystick.direction * _speed;
            _rotation = Quaternion.LookRotation(Vector3.forward, _rgb.velocity);
            _animator.enabled = true;

            transform.rotation = _rotation;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Bullet") {
                _HP -= 1;
                Events.OnPlayerGetDemage.Invoke(_HP / _MaxHP);
            }
        }
    }
}