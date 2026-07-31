using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Command;

// Problem Solved: Full Multi-Level Undo/Redo Text Editor Architecture
public interface ICommand
{
    void Execute();
    void Undo();
}

public class TextEditor
{
    public string Content { get; private set; } = string.Empty;

    public void Write(string text) => Content += text;

    public void Erase(int charCount)
    {
        if (charCount > Content.Length) charCount = Content.Length;
        Content = Content[..^charCount];
    }

    public void Display() => Console.WriteLine($"  Editor Content: \"{Content}\"");
}

public class WriteCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly string _text;

    public WriteCommand(TextEditor editor, string text)
    {
        _editor = editor;
        _text = text;
    }

    public void Execute()
    {
        _editor.Write(_text);
        Console.WriteLine($"  [Execute] Wrote \"{_text}\"");
    }

    public void Undo()
    {
        _editor.Erase(_text.Length);
        Console.WriteLine($"  [Undo] Erased \"{_text}\"");
    }
}

public class EraseCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly int _charCount;
    private string _erasedText = string.Empty;

    public EraseCommand(TextEditor editor, int charCount)
    {
        _editor = editor;
        _charCount = charCount;
    }

    public void Execute()
    {
        int actualCount = Math.Min(_charCount, _editor.Content.Length);
        _erasedText = _editor.Content[^actualCount..];
        _editor.Erase(_charCount);
        Console.WriteLine($"  [Execute] Erased {actualCount} chars (\"{_erasedText}\")");
    }

    public void Undo()
    {
        _editor.Write(_erasedText);
        Console.WriteLine($"  [Undo] Restored \"{_erasedText}\"");
    }
}

public class CommandManager
{
    private readonly Stack<ICommand> _history = new();
    private readonly Stack<ICommand> _redoStack = new();

    public void Execute(ICommand command)
    {
        command.Execute();
        _history.Push(command);
        _redoStack.Clear();
    }

    public void Undo()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("  [Undo] Nothing to undo.");
            return;
        }
        var command = _history.Pop();
        command.Undo();
        _redoStack.Push(command);
    }

    public void Redo()
    {
        if (_redoStack.Count == 0)
        {
            Console.WriteLine("  [Redo] Nothing to redo.");
            return;
        }
        var command = _redoStack.Pop();
        command.Execute();
        _history.Push(command);
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== COMMAND PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Encapsulating editor operations to enable full Undo/Redo\n");

        var editor = new TextEditor();
        var manager = new CommandManager();

        manager.Execute(new WriteCommand(editor, "Hello"));
        editor.Display();

        manager.Execute(new WriteCommand(editor, " World"));
        editor.Display();

        Console.WriteLine("\n--- Undoing ---");
        manager.Undo();
        editor.Display();

        Console.WriteLine("\n--- Redoing ---");
        manager.Redo();
        editor.Display();

        Console.WriteLine("============================\n");
    }
}
