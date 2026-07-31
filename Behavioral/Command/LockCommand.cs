using System;

namespace DesignPatterns.Behavioral.Command;

public class LockCommand(SmartLock lockHardware, bool shouldLock) : ISmartHomeCommand
{
    private readonly SmartLock _lock = lockHardware;
    private readonly bool _shouldLock = shouldLock;
    private bool _previousState;

    public string Description => $"{(_shouldLock ? "Lock" : "Unlock")} Door '{_lock.DoorName}'";

    public void Execute()
    {
        _previousState = _lock.IsLocked;
        if (_shouldLock) _lock.Lock();
        else _lock.Unlock();
    }

    public void Undo()
    {
        Console.WriteLine($"  ↩️ Undo -> {Description}");
        if (_previousState) _lock.Lock();
        else _lock.Unlock();
    }
}
