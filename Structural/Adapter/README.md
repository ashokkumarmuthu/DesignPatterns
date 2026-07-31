# Adapter Pattern 🔌

> *"Convert the interface of a class into another interface that clients expect. Adapter lets classes work together that couldn't otherwise because of incompatible interfaces."*

---

## 📋 Problem Statement
**Scenario:** Your web application's payment billing service (`CheckoutService`) expects all payment gateways to implement `IPaymentGateway`:
```csharp
public interface IPaymentGateway {
    bool ProcessPayment(decimal amount, string currency);
}
```
You are integrating a legacy banking SDK (`LegacyBankSdk`) which cannot be modified:
```csharp
public class LegacyBankSdk {
    public int MakePayment(double amount, int currencyCode) { ... }
}
```
`LegacyBankSdk` takes `double` (instead of `decimal`), `int` currency code (e.g. `840` for USD, `356` for INR instead of string `"USD"`), and returns `0` for success.

### The Naive / Bad Approach (Without Adapter):
Polluting your core `CheckoutService` with `if/else` branches to check `if (gateway is LegacyBankSdk)` breaks your system's clean payment abstraction.

---

## 💡 How Adapter Solves the Problem
1. **`LegacyBankAdapter`:** Implements `IPaymentGateway`.
2. Encapsulates a reference to `LegacyBankSdk`.
3. Inside `ProcessPayment(decimal, string)`, converts types (`decimal` → `double`, `"USD"` → `840`), calls `MakePayment()`, and translates integer status code `0` to boolean `true`.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Integrates incompatible legacy/third-party SDKs without modifying their source code or breaking client contracts.
- **Cons:** Introduces a translation layer indirection.

---

## 💻 C# Implementation Details
See [AdapterDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Structural/Adapter/AdapterDemo.cs) for complete executable code.
