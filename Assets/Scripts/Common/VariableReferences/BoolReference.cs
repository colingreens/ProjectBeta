using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    [Serializable]
    public class BoolReference
    {
        public bool useConstant = true;
        public bool constant;
        public BoolVariable variable;

        public bool Value
        {
            get { return useConstant ? constant : variable.value; }
        }
    }
}
