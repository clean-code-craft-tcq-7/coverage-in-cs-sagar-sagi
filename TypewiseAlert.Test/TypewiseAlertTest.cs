using System;
using Xunit;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {
    [Fact]
    public void InfersBreachAsPerLimits()
    {
      TypewiseAlert alertSystem = new TypewiseAlert();
      Assert.True(alertSystem.CheckForTemperatureBreach(12, 20, 30) ==
        TypewiseAlert.BreachType.TOO_LOW);
    }
  }
}
