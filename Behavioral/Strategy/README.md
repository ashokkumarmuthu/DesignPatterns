# Strategy Pattern 🎯

> *"Define a family of algorithms, encapsulate each one, and make them interchangeable."*

---

## 📋 Problem Statement
**Scenario:** You are building an e-commerce order fulfillment platform. Depending on customer preference or delivery urgency, orders can be shipped via:
1. **Standard Shipping:** $1.50 per kg (5-7 business days)
2. **Express Shipping:** $3.00 per kg + $5.00 flat surcharge (2-3 business days)
3. **Overnight Rush:** $5.00 per kg + $15.00 rush fee (next day delivery)

### The Naive / Bad Approach (Without Strategy):
```csharp
public decimal CalculateShipping(string method, double weight)
{
    if (method == "Standard") return (decimal)(weight * 1.5);
    else if (method == "Express") return (decimal)(weight * 3.0 + 5);
    else if (method == "Overnight") return (decimal)(weight * 5.0 + 15);
    // Every new method = another else-if = violation of OCP
}
```
**Why this breaks under real-world change:**
- Adding a new shipping partner (e.g. Same-Day Drone Delivery) forces you to modify existing tested methods.
- Testing each pricing formula independently is impossible because all logic is locked inside one monolithic method.

---

## 💡 How Strategy Solves the Problem
1. **`IShippingCalculator` (Interface):** Defines the common contract for calculating shipping costs.
2. **Concrete Strategies:** `StandardShipping`, `ExpressShipping`, and `OvernightShipping` implement `IShippingCalculator` as independent, self-contained classes.
3. **`OrderService` (Context):** Holds a reference to `IShippingCalculator`. At checkout or runtime, the active shipping strategy is injected without modifying `OrderService`.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Open for Extension / Closed for Modification (OCP compliant); isolated unit testing per strategy; runtime algorithm swapping.
- **Cons:** Increases class count; client code must select/inject the appropriate strategy.

---

## 💻 C# Implementation Details
See [StrategyDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Behavioral/Strategy/StrategyDemo.cs) for complete executable code.
