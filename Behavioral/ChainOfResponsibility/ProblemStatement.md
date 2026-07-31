# Chain of Responsibility Pattern Challenge: Corporate Expense Approval Pipeline 💰

### Scenario
You are building the expense management system for a large corporation. When an employee submits an expense claim, it must be routed through an approval hierarchy based on the amount:

| Approver Level | Approval Limit |
|:---|:---|
| **Team Lead** | Up to $500 |
| **Manager** | Up to $5,000 |
| **Director** | Up to $25,000 |
| **CFO** | Unlimited |

Each approver either approves the expense (if within their limit) or escalates it to the next person in the chain. The submitter doesn't need to know who ultimately approves their claim.

---

### What it teaches:
* **The Chain of Responsibility Pattern:** Decoupling the sender of a request from its receivers by giving multiple handlers a chance to process it.
* **Dynamic Chain Configuration:** The chain can be rearranged, extended, or shortened at runtime without modifying handler code.
* **Single Responsibility:** Each handler only knows its own approval logic and how to pass requests forward.

---

### Core Requirements
1. **Domain Models:**
   - `ExpenseClaim`: Employee name, department, amount, description, category
   - `ApprovalResult`: Approver name/title, amount, status, comments
2. **Abstract Handler (`ExpenseApprover`):**
   - `SetNext()` — chains the next approver
   - `Handle(ExpenseClaim)` — approves or escalates
3. **Concrete Handlers:**
   - `TeamLeadApprover` (≤ $500)
   - `ManagerApprover` (≤ $5,000)
   - `DirectorApprover` (≤ $25,000)
   - `CfoApprover` (unlimited)
