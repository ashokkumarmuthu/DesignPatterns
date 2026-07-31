using System;

namespace DesignPatterns.Behavioral.Command;

public class ThermostatCommand(SmartThermostat thermostat, int targetTemp) : ISmartHomeCommand
{
    private readonly SmartThermostat _thermostat = thermostat;
    private readonly int _targetTemp = targetTemp;
    private int _previousTemp;

    public string Description => $"Set Thermostat '{_thermostat.Room}' to {_targetTemp}°F";

    public void Execute()
    {
        _previousTemp = _thermostat.TargetTemp;
        _thermostat.SetTemperature(_targetTemp);
    }

    public void Undo()
    {
        Console.WriteLine($"  ↩️ Undo -> {Description}");
        _thermostat.SetTemperature(_previousTemp);
    }
}
