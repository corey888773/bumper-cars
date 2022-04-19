using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Czolgi {

    public class HealthBar : MonoBehaviour {
        [SerializeField] private Slider _slider;

        private void Awake() {
            Events.OnPlayerGetDemage += SetPlayerHealth;
        }

        private void SetPlayerHealth(float hp) {
            Debug.Log("wykonuje sie");
            _slider.value = hp;
        }

    }
}