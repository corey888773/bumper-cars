using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Czolgi {

    public static class Events {
        public static Action<float> OnPlayerGetDemage; // Podajesz znormalizowaną wartość życia playera (od 0 do 1)
        public static Action OnExplosionApper;
    }
}