# Facade Pattern Challenge: Enterprise Hotel Booking Platform 🏨

### Scenario
You are building the backend for a hotel reservation platform. A single room booking requires coordination across **four independent subsystems**:

1. **Room Inventory Service** — Checks room availability across property floors, manages room pool, handles overbooking protection.
2. **Payment Processing Service** — Validates credit card, processes charges, handles payment failures and refunds.
3. **Guest Registration Service** — Creates guest profiles, assigns loyalty points, records check-in history.
4. **Notification Service** — Sends booking confirmation emails, SMS alerts, and push notifications.

Without a Facade, every client (web app, mobile API, kiosk terminal) must individually call and coordinate all 4 services in the correct order, handle partial failures, and manage rollback logic.

---

### What it teaches:
* **The Facade Design Pattern:** Wrapping complex multi-service orchestration behind a single, clean `BookRoom()` method.
* **Decoupling Clients from Subsystems:** Frontend code never needs to know about payment processing internals or room inventory management.
* **Single Responsibility for Orchestration:** The Facade owns the "how" of booking coordination; subsystems own their individual domain logic.

---

### Core Requirements
1. **Subsystem Interfaces:**
   - `IRoomInventory`: `CheckAvailability()`, `ReserveRoom()`, `ReleaseRoom()`
   - `IPaymentService`: `ChargeCard()`, `RefundCard()`
   - `IGuestRegistry`: `RegisterGuest()`
   - `INotificationService`: `SendConfirmationEmail()`
2. **Facade (`HotelBookingFacade`):**
   - Single `BookRoom(BookingRequest)` method orchestrating all 4 subsystems
   - Returns `BookingConfirmation` with status, room number, and total charged
3. **Domain Models:**
   - `BookingRequest`: Guest name, email, room type, dates, payment info
   - `BookingConfirmation`: Confirmation ID, room assignment, total cost, status
