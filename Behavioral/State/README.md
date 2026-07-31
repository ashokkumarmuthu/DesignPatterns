# State Pattern 📦

> *"Allow an object to alter its behavior when its internal state changes. The object will appear to change its class."*

---

## 📋 Problem Statement
**Scenario:** An e-commerce order goes through states: Pending → Confirmed → Shipped → Delivered. Each state determines which actions are valid (e.g., you can't cancel a shipped order). Invalid transitions must be cleanly rejected.

### The Naive / Bad Approach (Without State):
```csharp
public void Ship(Order order)
{
    if (order.Status == "Pending")
        throw new Exception("Can't ship — not confirmed");
    else if (order.Status == "Confirmed")
        order.Status = "Shipped";
    else if (order.Status == "Shipped")
        throw new Exception("Already shipped");
    else if (order.Status == "Delivered")
        throw new Exception("Already delivered");
    else if (order.Status == "Cancelled")
        throw new Exception("Order cancelled");
    // Repeat this for EVERY action: Confirm(), Deliver(), Cancel()...
}
```
**Why this breaks:**
- Every action method has the same massive `if/else` block checking all possible states.
- Adding a new state (e.g., "ReturnRequested") requires editing every method.
- State transition logic is scattered across the codebase instead of being encapsulated.

---

## 💡 How State Solves the Problem
1. **`IOrderState` (Interface):** Defines all possible actions: `Confirm()`, `Ship()`, `Deliver()`, `Cancel()`.
2. **Concrete States (`PendingState`, `ConfirmedState`, etc.):** Each state class implements ONLY the behavior valid for that state and rejects invalid actions.
3. **`Order` (Context):** Holds a reference to the current `IOrderState` and delegates all actions to it. State objects themselves call `order.TransitionTo(newState)` to change the context's state.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Eliminates complex conditionals; each state is independently testable; adding new states doesn't modify existing ones (OCP).
- **Cons:** Increases class count (one class per state); state objects need reference to the context; overkill for simple 2-state scenarios.

---

## 💻 C# Implementation Details
See [Order.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/State/Order.cs) for the context class and [PendingState.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/State/PendingState.cs), [ConfirmedState.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/State/ConfirmedState.cs), [ShippedState.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/State/ShippedState.cs), [DeliveredState.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/State/DeliveredState.cs) for the concrete states.
