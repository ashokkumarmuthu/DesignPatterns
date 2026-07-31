# Observer Pattern 🔔

> *"Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically."*

---

## 📋 Problem Statement
**Scenario:** You are building a stock market tracking system for Apple Inc. (`AAPL`). Multiple components need live price updates whenever the stock price fluctuates:
1. **Individual Investors:** Need push notifications on price changes.
2. **Automated Trading Bots:** Need to automatically execute BUY orders when the price drops below a safety threshold ($140).
3. **Analytics Dashboards:** Need to recalculate charts.

### The Naive / Bad Approach (Without Observer):
```csharp
public class Stock {
    public void SetPrice(decimal newPrice) {
        _price = newPrice;
        investor1.SendAlert(newPrice);
        investor2.SendAlert(newPrice);
        tradingBot.CheckAutoBuy(newPrice);
        // Every new consumer forces us to edit the Stock class!
    }
}
```
**Why this breaks under real-world change:**
- The `Stock` class is tightly coupled to concrete subscriber objects (`Investor`, `TradingBot`).
- Subscribing or unsubscribing at runtime is impossible.

---

## 💡 How Observer Solves the Problem
1. **`IObserver` (Subscriber Interface):** Defines `Update(string stockSymbol, decimal price)`.
2. **`Stock` (Publisher / Subject):** Maintains a list of `IObserver` references. Provides `Attach(IObserver)` and `Detach(IObserver)`.
3. When `SetPrice()` is called, `Stock` loops through `_observers` and notifies everyone dynamically without knowing their exact concrete types.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Loose coupling between publisher and subscribers; dynamic runtime subscribe/unsubscribe.
- **Cons:** Memory leaks if observers fail to detach (`Detach`); notification order is not guaranteed.

---

## 💻 C# Implementation Details
See [ObserverDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Behavioral/Observer/ObserverDemo.cs) for complete executable code.
