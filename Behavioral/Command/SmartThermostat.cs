using System;

namespace DesignPatterns.Behavioral.Command;

public class SmartThermostat(string room)
{
    public string Room { get; } = room;
    public int TargetTemp { get; private set; } = 72;

    public void SetTemperature(int temp)
    {
        int prev = TargetTemp;
        TargetTemp = temp;
        Console.WriteLine($"  [Thermostat '{Room}'] Temperature adjusted from {prev}°F to {TargetTemp}°F");
    }
}
