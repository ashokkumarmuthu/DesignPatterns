# Strategy Pattern Challenge: Enterprise E-Commerce Logistics & Shipping Engine 📦

### Scenario
You are senior backend engineer at a global e-commerce enterprise. The platform ships millions of packages daily across international borders using multiple carrier networks (FedEx, DHL, Amazon Logistics). 

Each carrier calculates shipping rates using drastically different domain rules:
- **FedEx Standard:** $5.00 base + $1.50 per kg + distance multiplier.
- **DHL Express International:** $25.00 base fee + $4.00 per kg + customs tax rate (+15% for overseas destinations).
- **Amazon Prime Same-Day:** Flat $9.99 for orders under 5 kg; free for prime members with orders over $50; $12.50 flat otherwise.

The logistics team frequently adds new carriers (e.g. Uber Direct, Local Courier Services) and dynamic seasonal rate adjustments.

---

### What it teaches:
* **The Strategy Design Pattern:** Encapsulating variable carrier rate algorithms behind an `IShippingStrategy` contract.
* **Open/Closed Principle (OCP):** Adding a new carrier rate rule without modifying the core `ShippingCalculatorEngine`.
* **Dependency Injection & Runtime Strategy Switching:** Dynamically selecting the optimal or user-chosen carrier strategy at runtime.

---

### Core Requirements
1. **Domain Models:**
   - `ShipmentOrder`: Contains Order ID, weight in kg, distance in miles, destination country, total order value, and is-prime flag.
   - `ShippingQuote`: Contains Carrier name, calculated rate, estimated delivery days, and breakdown notes.
2. **Strategy Contract (`IShippingStrategy`):**
   - `CarrierName` (property)
   - `CalculateQuote(ShipmentOrder order)` -> returns `ShippingQuote`
3. **Concrete Strategies:**
   - `FedExStandardStrategy`
   - `DhlExpressStrategy`
   - `AmazonSameDayStrategy`
4. **Context Engine (`LogisticsEngine`):**
   - Maintains active strategy or accepts strategy list to compare quotes.
   - Supports selecting the lowest-cost carrier automatically across all registered strategies.
