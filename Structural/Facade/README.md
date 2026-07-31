# Facade Pattern 🏨

> *"Provide a simplified interface to a complex subsystem."*

---

## 📋 Problem Statement
**Scenario:** A hotel booking requires coordinating 4 independent subsystems — Room Inventory, Payment, Guest Registration, and Email Notification. Without a Facade, every client must manually call all services in the correct order and handle partial failures.

### The Naive / Bad Approach (Without Facade):
```csharp
// Every client repeats this 6-step orchestration!
var isAvailable = roomInventory.CheckAvailability("Deluxe", checkIn, checkOut);
if (!isAvailable) return "No rooms";
var charged = paymentService.ChargeCard(card, totalCost, "Booking");
if (!charged) return "Payment failed";
var room = roomInventory.ReserveRoom("Deluxe", checkIn, checkOut);
guestRegistry.RegisterGuest(name, email, room);
notificationService.SendConfirmationEmail(email, confirmId, room, checkIn, checkOut);
// Forgot rollback? Payment charged but room not reserved? 💥
```
**Why this breaks:**
- Every consumer (web, mobile, kiosk) duplicates the same complex orchestration logic.
- Adding a 5th subsystem (e.g., loyalty points) requires updating every client.
- Error handling and rollback logic is scattered everywhere.

---

## 💡 How Facade Solves the Problem
1. **`HotelBookingFacade`** wraps all subsystem calls behind a single `BookRoom(request)` method.
2. Clients only interact with the Facade — they don't know about inventory, payment, or notification internals.
3. Adding new subsystems (loyalty, analytics) only changes the Facade, not every client.

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Drastically simplifies client code; centralizes orchestration & error handling; subsystems remain independently testable.
- **Cons:** Facade can become a "God class" if it grows too large; doesn't prevent clients from accessing subsystems directly (use with access modifiers).

---

## 💻 C# Implementation Details
See [HotelBookingFacade.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Structural/Facade/HotelBookingFacade.cs) for complete implementation with 4 subsystem services.
