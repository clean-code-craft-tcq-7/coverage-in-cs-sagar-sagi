using System;
using System.Diagnostics;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public int lowerLimit = 0;
        public int upperLimit = 0;
        public double temperature = 0d;

        public enum BreachType
        {
            NORMAL,
            TOO_LOW,
            TOO_HIGH
        };

        public enum CoolingType
        {
            PASSIVE_COOLING,
            HI_ACTIVE_COOLING,
            MED_ACTIVE_COOLING
        };

        public enum AlertTarget
        {
            TO_CONTROLLER,
            TO_EMAIL
        };

        public struct BatteryCharacter
        {
            public CoolingType coolingType;
            public string brand;
        }

        public static void Main()
        {
            TypewiseAlert alertSystem = new TypewiseAlert();

            BatteryCharacter batteryType1 = new BatteryCharacter
            {
                coolingType = CoolingType.PASSIVE_COOLING,
                brand = "Excide"
            };

            alertSystem.temperature = 50d;

            alertSystem.SetTemperatureRange(batteryType1.coolingType);

            Debug.Assert(alertSystem.upperLimit != 0);
            Console.WriteLine("TUL " + alertSystem.upperLimit);

            BreachType breachType = alertSystem.CheckForTemperatureBreach(alertSystem.temperature, alertSystem.lowerLimit, alertSystem.upperLimit);

            Debug.Assert(breachType.Equals(BreachType.TOO_HIGH));

            if (breachType.Equals(BreachType.TOO_HIGH) || breachType.Equals(BreachType.TOO_LOW))
            {
                bool alertSent = alertSystem.AlertController(breachType);
                Debug.Assert(alertSent == true);

                alertSent = alertSystem.AlertEmail(breachType);
                Debug.Assert(alertSent == true);
            }
        }

        public void SetTemperatureRange(CoolingType coolingType)
        {
            switch (coolingType)
            {
                case CoolingType.PASSIVE_COOLING:
                    lowerLimit = 0;
                    upperLimit = 35;
                    break;
                case CoolingType.HI_ACTIVE_COOLING:
                    lowerLimit = 0;
                    upperLimit = 45;
                    break;
                case CoolingType.MED_ACTIVE_COOLING:
                    lowerLimit = 0;
                    upperLimit = 40;
                    break;
            }
        }


        public BreachType CheckForTemperatureBreach(double temperatureInC, int lowerLimit, int upperLimit)
        {
            if (temperatureInC < lowerLimit)
            {
                return BreachType.TOO_LOW;
            }
            if (temperatureInC > upperLimit)
            {
                return BreachType.TOO_HIGH;
            }
            return BreachType.NORMAL;
        }

        public bool AlertController(BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine("{0} : {1}\n", header, breachType);
            return true; //assuming alert sent
        }


        public bool AlertEmail(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            bool mailSent = false;

            switch (breachType)
            {
                case BreachType.TOO_LOW:
                    Console.WriteLine("To: {0}\n", recepient);
                    Console.WriteLine("Hi, the temperature is too low\n");
                    mailSent = true;
                    break;
                case BreachType.TOO_HIGH:
                    Console.WriteLine("To: {0}\n", recepient);
                    Console.WriteLine("Hi, the temperature is too high\n");
                    mailSent = true;
                    break;
                case BreachType.NORMAL:
                    break;
            }
            return mailSent;
        }
    }
}
