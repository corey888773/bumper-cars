using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Czolgi
{
    public class TurretRotation : MonoBehaviour
    {
        public float speedOfRotation = 10.0f;
        private Transform _transform;
        private Vector2 _padPosition = Vector2.zero;
        private Quaternion _target = new Quaternion(0, 0, 0, 0);
        void Start()
        {
            _transform = GetComponent<Transform>();
        }
        
        void FixedUpdate()
        {
            _padPosition.x = FireJoystick.horizontal;
            _padPosition.y = FireJoystick.vertical;
            if (_padPosition != Vector2.zero)
            {
                _target = Quaternion.LookRotation(Vector3.forward, _padPosition);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _target, speedOfRotation * Time.deltaTime);
        }
    }
}
