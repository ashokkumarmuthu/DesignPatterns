using System;

namespace DesignPatterns.Behavioral.Command;

public class SmartLight(string name)
{
    public string Name { get; } = name;
    public bool IsOn { get; private set; }
    public int Brightness { get; private set; } = 100;

    public void TurnOn() { IsOn = true; Console.WriteLine($"  [Light '{Name}'] Powered ON (Brightness: {Brightness}%)"); }
    public void TurnOff() { IsOn = false; Console.WriteLine($"  [Light '{Name}'] Powered OFF"); }
    public void SetBrightness(int level)
    {
        Brightness = level;
        Console.WriteLine($"  [Light '{Name}'] Brightness set to {Brightness}%");
    }
}
