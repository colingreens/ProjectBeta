﻿using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Metroidvania/Player/Health", order = 3)]
    public class HealthConfig : ScriptableObject
    {
        public int HealthPoints = 3;
        public int CurrentHealthPoints;
    }
}