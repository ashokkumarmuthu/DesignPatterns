using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Command;

public class MacroCommand(string macroName, List<ISmartHomeCommand> commands) : ISmartHomeCommand
{
    public string Description => $"Macro Routine: '{macroName}' ({_commands.Count} actions)";
    private readonly List<ISmartHomeCommand> _commands = commands;

    public void Execute()
    {
        Console.WriteLine($"\n🎬 [Executing Macro: '{macroName}']");
        foreach (var cmd in _commands)
        {
            cmd.Execute();
        }
    }

    public void Undo()
    {
        Console.WriteLine($"\n↩️ [Undoing Macro: '{macroName}'] (Executing undo in reverse order)");
        for (int i = _commands.Count - 1; i >= 0; i--)
        {
            _commands[i].Undo();
        }
    }
}
