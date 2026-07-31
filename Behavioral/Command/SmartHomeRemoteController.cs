using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Command;

public class SmartHomeRemoteController
{
    private readonly Stack<ISmartHomeCommand> _undoStack = new();

    public void PressButton(ISmartHomeCommand command)
    {
        Console.WriteLine($"\n🔘 Remote Pressed -> {command.Description}");
        command.Execute();
        _undoStack.Push(command);
    }

    public void PressUndoButton()
    {
        if (_undoStack.Count == 0)
        {
            Console.WriteLine("\n⚠️ [Remote] Nothing to undo!");
            return;
        }

        var lastCommand = _undoStack.Pop();
        Console.WriteLine($"\n⏮️ Remote UNDO Pressed!");
        lastCommand.Undo();
    }
}
