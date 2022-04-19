using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {

    public class CameraShake : MonoBehaviour {
        [SerializeField] float _duration = 0.4f;
        [SerializeField] float _magnitude = 0.001f;

        private void Awake() {
            Events.OnExplosionApper += CreateShake;
        }

        public void CreateShake() {
            StartCoroutine(Shake());
        }

        public IEnumerator Shake() {
            Vector3 orginalPos = transform.localPosition;
            float timeCount = 0;
            while (timeCount < _duration) {
                timeCount += Time.deltaTime;

                float x = Random.Range(-1, 1) * _magnitude;
                float y = Random.Range(-1, 1) * _magnitude;

                transform.localPosition = new Vector3(orginalPos.x + x, orginalPos.y + y, orginalPos.z);

                yield return null;

            }
            transform.localPosition = orginalPos;
        }
    }
}