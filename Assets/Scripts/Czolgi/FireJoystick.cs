using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {
    public class FireJoystick : BaseJoystick {
        public static Vector2 direction = Vector2.zero;

        private new void Start() {
            base.Start();
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