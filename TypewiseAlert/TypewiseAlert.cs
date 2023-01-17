using System;

namespace TypewiseAlert
{
    public class TypewiseAlert : IAlertTarget
    {
        public delegate void SendEmailAlert();
        public static SendEmailAlert sendEmailAlert;
        public enum BreachType
        {
            NORMAL,
            TOO_LOW,
            TOO_HIGH
        };
        public static BreachType inferBreach(double value, double lowerLimit, double upperLimit)
        {
            if (value < lowerLimit)
            {
                sendEmailAlert = SendEmailAlertForLowBreach;
                return BreachType.TOO_LOW;
            }
            if (value > upperLimit)
            {
                sendEmailAlert = SendEmailAlertForHighBreach;
                return BreachType.TOO_HIGH;
            }
            return BreachType.NORMAL;
        }

        public static BreachType classifyTemperatureBreach(
            ICoolingType coolingType, double temperatureInC)
        {
            int lowerLimit = 0;
            int upperLimit = 0;

            coolingType.SetTemperatureLimits(out lowerLimit, out upperLimit);

            return inferBreach(temperatureInC, lowerLimit, upperLimit);
        }

        public enum AlertTarget
        {
            TO_CONTROLLER,
            TO_EMAIL
        };

        public static void checkAndAlert(
            AlertTarget alertTarget, ICoolingType coolingType, double temperatureInC, IAlertTarget alerter)
        {

            BreachType breachType = classifyTemperatureBreach(
              coolingType, temperatureInC
            );

            switch (alertTarget)
            {
                case AlertTarget.TO_CONTROLLER:
                    alerter.ControllerAlert(breachType);
                    break;
                case AlertTarget.TO_EMAIL:
                    alerter.EmailAlert(breachType);
                    break;
            }
        }

        public void EmailAlert(BreachType breachType)
        {
            if (breachType != BreachType.NORMAL)
            {
                sendEmailAlert();
            }
        }

        public static void SendEmailAlertForLowBreach()
        {
            string recepient = "a.b@c.com";
            Console.WriteLine("To: {}\n", recepient);
            Console.WriteLine("Hi, the temperature is too low\n");
        }

        public static void SendEmailAlertForHighBreach()
        {
            string recepient = "a.b@c.com";
            Console.WriteLine("To: {}\n", recepient);
            Console.WriteLine("Hi, the temperature is too high\n");
        }

        public void ControllerAlert(BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine("{} : {}\n", header, breachType);
        }
    }
}