using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public class HighActiveCooling : ICoolingType
    {
        public void SetTemperatureLimits(out int LowerLimit, out int UpperLimit)
        {
            LowerLimit = 0;
            UpperLimit = 45;
        }
    }
}
