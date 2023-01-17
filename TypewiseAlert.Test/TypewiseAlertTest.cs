using System;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest : IAlertTarget
    {
        public int EmailAlertCount = 0;
        public int ControllerAlertCount = 0;


        TypewiseAlertTest typewiseAlertTest = new TypewiseAlertTest();

        public void ControllerAlert(TypewiseAlert.BreachType breachType)
        {
            ControllerAlertCount++;
        }

        public void EmailAlert(TypewiseAlert.BreachType breachType)
        {
            EmailAlertCount++;
        }

        [Fact]
        public void InfersBreachAsPerLimits_Low()
        {
            Assert.True(TypewiseAlert.inferBreach(12, 20, 30) ==
              TypewiseAlert.BreachType.TOO_LOW);
        }

        [Fact]
        public void InfersBreachAsPerLimits_High()
        {
            Assert.True(TypewiseAlert.inferBreach(85, 25, 55) ==
              TypewiseAlert.BreachType.TOO_HIGH);
        }

        [Fact]
        public void InfersBreachAsPerLimits_Normal()
        {
            Assert.True(TypewiseAlert.inferBreach(25, 15, 45) ==
              TypewiseAlert.BreachType.NORMAL);
        }

        [Fact]
        public void ClassifyTempAndInfersBreach_PassiveCooling()
        {
            PassiveCooling passiveCooling = new PassiveCooling();
            Assert.True(TypewiseAlert.classifyTemperatureBreach(passiveCooling, 50) ==
              TypewiseAlert.BreachType.TOO_HIGH);
        }

        [Fact]
        public void ClassifyTempAndInfersBreach_HiActiveCooling()
        {
            HighActiveCooling highActiveCooling = new HighActiveCooling();
            Assert.True(TypewiseAlert.classifyTemperatureBreach(highActiveCooling, 85) ==
              TypewiseAlert.BreachType.TOO_HIGH);
        }

        [Fact]
        public void ClassifyTempAndInfersBreach_MedActiveCooling()
        {
            MedActiveCooling medActiveCooling = new MedActiveCooling();
            Assert.True(TypewiseAlert.classifyTemperatureBreach(medActiveCooling, 60) ==
              TypewiseAlert.BreachType.TOO_HIGH);
        }

        [Fact]
        public void AlertControllerDuringPassiveCooling()
        {
            PassiveCooling passiveCooling = new PassiveCooling();

            TypewiseAlert.checkAndAlert(TypewiseAlert.AlertTarget.TO_CONTROLLER, passiveCooling, 45d, typewiseAlertTest);

            Assert.True(ControllerAlertCount == 1);  //Assert if ControllerAlert is not invoked
            ControllerAlertCount = 0; //Reset here
        }

        [Fact]
        public void AlertEmailDuringPassiveCooling()
        {
            PassiveCooling passiveCooling = new PassiveCooling();

            TypewiseAlert.checkAndAlert(TypewiseAlert.AlertTarget.TO_EMAIL, passiveCooling, 45d, typewiseAlertTest);

            Assert.True(EmailAlertCount == 1);  //Assert if ControllerAlert is not invoked
            EmailAlertCount = 0; //Reset here
        }
    }
}