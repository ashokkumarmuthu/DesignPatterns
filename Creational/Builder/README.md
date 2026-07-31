# Builder Pattern 🔧

> *"Separate the construction of a complex object from its representation so that the same construction process can create different representations."*

---

## 📋 Problem Statement
**Scenario:** You are building an email client module. An `Email` object has many properties, several of which are optional (`To`, `From`, `Subject`, `Body`, `Cc`, `Bcc`, `IsHtml`, `Attachments`).

### The Naive / Bad Approach (Without Builder):
```csharp
// Unreadable "telescoping constructor"
var email = new Email("to@test.com", "from@test.com", null, null, "Subject", "Body", true, false);
```
**Why this breaks under real-world change:**
- It is impossible to tell what `null, null, true, false` mean without opening the constructor definition.
- Skipping optional parameters requires passing endless `null` values.

---

## 💡 How Builder Solves the Problem
1. **`EmailBuilder`:** Provides a fluent API (`.From()`, `.To()`, `.Subject()`, `.Cc()`, `.AsHtml()`, `.Attach()`).
2. Each method returns `this`, allowing clean method chaining.
3. The `.Build()` method validates mandatory fields and returns an immutable `Email` object.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Highly readable self-documenting code; handles optional parameters cleanly; guarantees valid object construction via `.Build()`.
- **Cons:** Requires writing and maintaining an extra `Builder` class.

---

## 💻 C# Implementation Details
See [BuilderDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Creational/Builder/BuilderDemo.cs) for complete executable code.
