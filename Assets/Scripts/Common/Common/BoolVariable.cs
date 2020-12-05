﻿using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "NewBoolVar", menuName = "CustomSO/Types/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool Value;
    }
}
