# State Pattern Challenge: E-Commerce Order Fulfillment Lifecycle 📦

### Scenario
You are building the order management system for an e-commerce platform. Every order goes through a strict lifecycle:

**Pending → Confirmed → Shipped → Delivered**

Each state determines which actions are valid:
- **Pending:** Can be confirmed or cancelled. Cannot be shipped or delivered.
- **Confirmed:** Can be shipped or cancelled. Cannot be confirmed again or delivered directly.
- **Shipped:** Can be delivered. Cannot be cancelled (already in transit), confirmed, or re-shipped.
- **Delivered:** Terminal state. No further actions allowed (return process is separate).
- **Cancelled:** Terminal state. No further actions allowed.

The naive approach uses deeply nested `if/switch` blocks checking the current status string, leading to fragile, unmaintainable code. The State pattern eliminates this entirely.

---

### What it teaches:
* **The State Design Pattern:** Encapsulating state-specific behavior into separate classes, eliminating complex conditionals.
* **State Transitions:** State objects themselves perform the transition by calling `order.TransitionTo(newState)`.
* **Invalid Transition Handling:** Each state cleanly rejects actions that don't apply, with clear error messages.

---

### Core Requirements
1. **State Interface (`IOrderState`):**
   - `Confirm()`, `Ship()`, `Deliver()`, `Cancel()`, `StatusName`
2. **Concrete States:**
   - `PendingState`, `ConfirmedState`, `ShippedState`, `DeliveredState`, `CancelledState`
3. **Context (`Order`):**
   - Holds current `IOrderState`, delegates all actions to it
   - `TransitionTo(IOrderState)` method for state changes
