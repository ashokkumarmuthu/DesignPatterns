# Singleton Pattern 🔒

> *"Ensure a class has only one instance, and provide a global point of access to it."*

---

## 📋 Problem Statement
**Scenario:** Your application needs an centralized logger (`AppLogger`) to write log statements across multiple concurrent background workers.

### The Naive / Bad Approach (Without Singleton):
Allowing any service to instantiate `new AppLogger()` leads to multiple conflicting logger instances, file lock contention on disk, and inconsistent log counts.

---

## 💡 How Singleton Solves the Problem
1. **Private Constructor:** Prevents direct instantiation via `new`.
2. **`Lazy<AppLogger>`:** Guarantees thread-safe, lazy initialization on first access.
3. **`AppLogger.Instance`:** Global static entry point returning the single shared instance.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Guaranteed single instance; thread-safe lazy loading.
- **Cons:** Introduces global state which can complicate unit testing. (*Note: In modern .NET, prefer `services.AddSingleton<T>()` via Dependency Injection container*).

---

## 💻 C# Implementation Details
See [SingletonDemo.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetTest/DesignPatterns/Creational/Singleton/SingletonDemo.cs) for complete executable code.
