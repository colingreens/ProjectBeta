using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroidVaniaTools
{
    [Serializable]
    public class FloatReference
    {
        public bool useConstant = true;
        public float constant;
        public FloatVariable variable;
        // No setter for value intended.
        public float Value
        {
            get { return useConstant ? constant : variable.Value; }
        }
    }
}
