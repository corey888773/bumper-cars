using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Czolgi {
    public class Shooting : MonoBehaviour {
        public float bulletSpeed;
        public Transform shootPos;
        public Transform turret;
        public GameObject bullet;
        public float clicksDelay;
        public float shootingDelay;
        private float _lastClickTime, _lastShootTime;
        private int _clicks;

        void Update() {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                float now = Time.time;
                _clicks++;
                if (_clicks == 2) {
                    if (now - _lastClickTime < clicksDelay && now - _lastShootTime > shootingDelay) {
                        Shoot();
                        _lastShootTime = now;
                    }
                    _clicks = 0;
                }
                _lastClickTime = now;
            }
        }

        void Shoot() {
            Quaternion rotation = turret.rotation;
            GameObject newBullet = Instantiate(bullet, shootPos.position, rotation);
            Rigidbody2D rigidb = newBullet.GetComponent<Rigidbody2D>();
            Vector3 direction = turret.up;
            rigidb.velocity = direction * bulletSpeed;
        }
    }
}