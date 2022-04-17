using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {
    public class FireJoystick : BaseJoystick {
        public static Vector2 direction = Vector2.zero;
        [SerializeField] private int _padding = 15;

        private new void Start() {
            base.Start();
            transform.position = _camera.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax - _joystickRadiusInPixels - _padding, _joystickRadiusInPixels + _padding, 0));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            _joystickPos = _camera.WorldToScreenPoint(transform.position);
        }

        private new void Update() {
            base.Update();
            UpdateJoystickDirection();
        }

        private void UpdateJoystickDirection() {
            direction = new Vector2(_horizontal, _vertical).normalized;
        }
    }
}