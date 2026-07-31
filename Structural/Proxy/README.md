# Proxy Pattern 🔐

> *"Provide a surrogate or placeholder for another object to control access to it."*

---

## 📋 Problem Statement
**Scenario:** A document vault stores sensitive corporate files with varying classification levels (Public, Internal, Confidential, Restricted). Access must be controlled by user role, and every access attempt must be audit-logged — but the real vault shouldn't be modified.

### The Naive / Bad Approach (Without Proxy):
```csharp
public Document GetDocument(string id, User user)
{
    // Access control logic mixed into the vault itself
    if (doc.Classification == "Restricted" && user.Role != "Admin")
        throw new UnauthorizedAccessException();
    // Audit logging mixed into the vault
    _auditLog.Add(new LogEntry(user, id, DateTime.Now));
    return _documents[id];
    // Real storage logic, access control, and logging are all tangled together!
}
```
**Why this breaks:**
- The vault class violates Single Responsibility — it handles storage, access control, AND logging.
- Testing storage logic requires dealing with access control and logging dependencies.
- Adding new cross-cutting concerns (caching, rate limiting) keeps bloating the vault class.

---

## 💡 How Proxy Solves the Problem
1. **`IDocumentVault` (Subject Interface):** Defines the common interface for both the real vault and the proxy.
2. **`ConfidentialDocumentVault` (Real Subject):** Pure document storage and retrieval — no access control or logging.
3. **`SecureDocumentProxy` (Proxy):** Wraps the real vault and transparently adds role-based access checks and audit logging before delegating to the real vault.

The client code works with `IDocumentVault` and never knows whether it's talking to the real vault or the proxy.

---

## 🔧 Types of Proxies
| Type | Purpose | Example |
|:---|:---|:---|
| **Protection Proxy** | Access control | Role-based document access (this example) |
| **Logging Proxy** | Audit trails | Recording all access attempts (this example) |
| **Virtual Proxy** | Lazy loading | Loading large images/files only when accessed |
| **Caching Proxy** | Performance | Caching expensive database query results |
| **Remote Proxy** | Network abstraction | gRPC/REST client wrapping remote service calls |

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Adds behavior without modifying the real object; transparent to clients via shared interface; separates cross-cutting concerns cleanly.
- **Cons:** Adds indirection; can obscure debugging; proxy must be kept in sync with the real subject's interface.

---

## 💻 C# Implementation Details
See [SecureDocumentVault.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Structural/Proxy/SecureDocumentVault.cs) for the full implementation with RBAC and audit logging.
