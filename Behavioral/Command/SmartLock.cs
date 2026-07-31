using System;

namespace DesignPatterns.Behavioral.Command;

public class SmartLock(string doorName)
{
    public string DoorName { get; } = doorName;
    public bool IsLocked { get; private set; } = true;

    public void Lock() { IsLocked = true; Console.WriteLine($"  [Lock '{DoorName}'] Deadbolt LOCKED 🔒"); }
    public void Unlock() { IsLocked = false; Console.WriteLine($"  [Lock '{DoorName}'] Deadbolt UNLOCKED 🔓"); }
}
