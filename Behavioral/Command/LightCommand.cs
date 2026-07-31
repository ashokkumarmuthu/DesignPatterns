using System;

namespace DesignPatterns.Behavioral.Command;

public class LightCommand(SmartLight light, bool turnOn, int brightness = 100) : ISmartHomeCommand
{
    private readonly SmartLight _light = light;
    private readonly bool _turnOn = turnOn;
    private readonly int _brightness = brightness;
    private bool _previousState;
    private int _previousBrightness;

    public string Description => $"Turn {(_turnOn ? "ON" : "OFF")} Light '{_light.Name}'";

    public void Execute()
    {
        _previousState = _light.IsOn;
        _previousBrightness = _light.Brightness;

        if (_turnOn)
        {
            _light.TurnOn();
            _light.SetBrightness(_brightness);
        }
        else
        {
            _light.TurnOff();
        }
    }

    public void Undo()
    {
        Console.WriteLine($"  ↩️ Undo -> {Description}");
        if (_previousState)
        {
            _light.TurnOn();
            _light.SetBrightness(_previousBrightness);
        }
        else
        {
            _light.TurnOff();
        }
    }
}
