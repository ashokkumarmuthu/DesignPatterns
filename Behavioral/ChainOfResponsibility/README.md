# Chain of Responsibility Pattern 💰

> *"Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. Chain the receiving objects and pass the request along until an object handles it."*

---

## 📋 Problem Statement
**Scenario:** An employee submits an expense claim. Depending on the amount, it must be approved by the right authority: Team Lead (≤$500), Manager (≤$5K), Director (≤$25K), or CFO (any amount). The submitter shouldn't know or care who approves it.

### The Naive / Bad Approach (Without Chain):
```csharp
public ApprovalResult ApproveExpense(ExpenseClaim claim)
{
    if (claim.Amount <= 500) return teamLead.Approve(claim);
    else if (claim.Amount <= 5000) return manager.Approve(claim);
    else if (claim.Amount <= 25000) return director.Approve(claim);
    else return cfo.Approve(claim);
    // Hard-coded routing. Adding VP level = editing this method.
}
```
**Why this breaks:**
- Routing logic is centralized and hard-coded — adding a new approval level requires modifying the router.
- The sender is tightly coupled to all possible handlers.
- No flexibility to reorder or skip levels dynamically.

---

## 💡 How Chain of Responsibility Solves the Problem
1. **`ExpenseApprover` (Abstract Handler):** Defines `Handle()` and `SetNext()`. Each handler either processes the request or passes it to the next handler.
2. **Concrete Handlers:** `TeamLeadApprover`, `ManagerApprover`, `DirectorApprover`, `CfoApprover` — each with their own approval limit.
3. **Chain Assembly:** `teamLead.SetNext(manager).SetNext(director).SetNext(cfo)` — the chain is built at runtime and can be reconfigured easily.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Decouples sender from receivers; chain can be assembled/reconfigured dynamically; each handler has single responsibility.
- **Cons:** No guarantee the request gets handled (unless a catch-all handler exists); debugging chain flow can be harder; performance overhead for long chains.

---

## 🌐 Real-World Usage
- **ASP.NET Core Middleware Pipeline** — each middleware is a handler in the chain
- **DOM Event Bubbling** — events propagate up the DOM tree
- **Logging Frameworks** — log levels (Debug → Info → Warn → Error) route through handler chains

---

## 💻 C# Implementation Details
See [ExpenseApprovalChain.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/ChainOfResponsibility/ExpenseApprovalChain.cs) for the complete implementation.
