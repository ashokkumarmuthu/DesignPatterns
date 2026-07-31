# Decorator Pattern 🎨

> *"Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality."*

---

## 📋 Problem Statement
**Scenario:** You are building a data access component (`FileDataReader`). You need to add optional, stackable behaviors at runtime depending on security or telemetry policies:
1. **Decryption:** Decrypt raw encrypted payload data.
2. **Logging:** Log audit traces when reads begin and finish.
3. **Execution Timing:** Measure execution duration in milliseconds.

### The Naive / Bad Approach (Without Decorator):
Subclassing for every combination of features (`EncryptedLoggingDataReader`, `EncryptedTimingLoggingDataReader`, etc.) leads to an exponential **subclass explosion** of rigid, non-stackable classes.

---

## 💡 How Decorator Solves the Problem
1. **`IDataReader` Interface:** Shared contract between core component and wrappers.
2. **`DataReaderDecorator` (Base Decorator):** Wraps an `IDataReader` reference and forwards `Read()` calls.
3. Concrete decorators (`EncryptionDecorator`, `LoggingDecorator`, `TimingDecorator`) extend `DataReaderDecorator` to inject cross-cutting concerns around the wrapped call. Wrappers can be nested arbitrarily at runtime!

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Prevents subclass explosion; stackable runtime behaviors (Open/Closed Principle compliant).
- **Cons:** Nested decorators can make stack traces slightly deeper to debug.

---

## 💻 C# Implementation Details
See [DecoratorDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Structural/Decorator/DecoratorDemo.cs) for complete executable code.
