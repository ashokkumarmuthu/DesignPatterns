# Command Pattern ⌨️

> *"Encapsulate a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations."*

---

## 📋 Problem Statement
**Scenario:** You are building a rich Text Editor (like MS Word or VS Code). The user performs text modifications (`Write`, `Erase`), and expects full **Undo (Ctrl+Z)** and **Redo (Ctrl+Y)** capability across their entire editing history.

### The Naive / Bad Approach (Without Command):
Directly mutating text strings inside button handlers make tracking what was typed or deleted extremely brittle, making true multi-level undo impossible without storing massive full-document snapshots.

---

## 💡 How Command Solves the Problem
1. **`ICommand` Interface:** Declares `Execute()` and `Undo()`.
2. **Concrete Commands (`WriteCommand`, `EraseCommand`):** Encapsulate the specific arguments (text written, characters erased) and store state needed to undo the exact operation.
3. **`CommandManager` (Invoker):** Maintains undo/redo `Stack<ICommand>` stacks. Calling `Undo()` pops the top command and invokes `command.Undo()`.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Robust multi-level Undo/Redo; transaction logging; decouples user triggers from core editor logic.
- **Cons:** Increases memory usage to retain command history stacks; creates multiple command classes.

---

## 💻 C# Implementation Details
See [CommandDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Behavioral/Command/CommandDemo.cs) for complete executable code.
