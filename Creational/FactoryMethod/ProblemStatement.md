# Factory Method Pattern Challenge: Multi-Tenant Payment Processor Dispatcher 💳

### Scenario
You are building an international payment gateway for a Fintech app. Depending on customer transaction parameters (e.g., region, currency, transaction limit), the system must route payment processing to different payment providers:
- **Stripe Processor:** For US/EUR Credit Cards (supports 3D Secure, zero setup fee).
- **PayPal Express:** For Global Wallet Payments (requires OAuth token handshake).
- **Razorpay Processor:** For INR (Indian Rupee) UPI and NetBanking transactions.

Each payment provider requires complex initialization sequence (fetching API keys from secrets manager, setting HTTP timeout headers, initializing OAuth session tokens). 

Client code calling `ProcessTransaction()` should not know about specific API keys or provider classes.

---

### What it teaches:
* **The Factory Method Design Pattern:** Subclasses decide which concrete payment processor object to instantiate.
* **Encapsulation of Complex Creation Logic:** Hiding secret configuration loading and SDK setup sequence behind a standard factory method contract.

---

### Core Requirements
1. **Domain Models:**
   - `PaymentRequest`: TransactionId, Amount, Currency, MerchantId.
   - `PaymentResult`: Success status, TransactionReference, ProviderName, Timestamp.
2. **Product Interface (`IPaymentProcessor`):**
   - `ProviderName` (property)
   - `ExecutePayment(PaymentRequest request)` -> returns `PaymentResult`
3. **Abstract Creator (`PaymentProcessorFactory`):**
   - `CreateProcessor()` -> Factory Method.
   - `Process(PaymentRequest request)` -> Template method that calls `CreateProcessor()` and executes payment.
4. **Concrete Creators:**
   - `StripeProcessorFactory`
   - `PayPalProcessorFactory`
   - `RazorpayProcessorFactory`
