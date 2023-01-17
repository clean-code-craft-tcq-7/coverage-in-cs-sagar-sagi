using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public interface IAlertTarget
    {
        void EmailAlert(TypewiseAlert.BreachType breachType);

        void ControllerAlert(TypewiseAlert.BreachType breachType);
    }
}
