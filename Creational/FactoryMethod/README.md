# Factory Method Pattern 🏭

> *"Define an interface for creating an object, but let subclasses decide which class to instantiate."*

---

## 📋 Problem Statement
**Scenario:** You are building an enterprise alert notification system. Alerts can be dispatched via `Email`, `SMS`, or `PushNotification`. Each notification channel requires different creation logic (API keys, SMTP servers, endpoints).

### The Naive / Bad Approach (Without Factory):
Scattering `new EmailNotification()`, `new SmsNotification()` logic across controllers, services, and background workers duplicates construction parameters and locks client code to concrete implementations.

---

## 💡 How Factory Method Solves the Problem
1. **`INotification` Interface:** Common product contract.
2. **`NotificationCreator` (Abstract Factory Class):** Declares abstract method `CreateNotification()`.
3. Concrete subclasses (`EmailNotificationCreator`, `SmsNotificationCreator`, `PushNotificationCreator`) implement `CreateNotification()`, hiding the instantiation details from client callers.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Centralizes complex instantiation logic; client code depends solely on interfaces (`INotification`); OCP compliant.
- **Cons:** Can create deep class hierarchies if overused for simple objects.

---

## 💻 C# Implementation Details
See [FactoryMethodDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Creational/FactoryMethod/FactoryMethodDemo.cs) for complete executable code.
