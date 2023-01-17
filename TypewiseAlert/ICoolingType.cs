using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public interface ICoolingType
    {
        void SetTemperatureLimits(out int LowerLimit, out int UpperLimit);
    }
}
