# Observer Pattern Challenge: Multi-Channel Financial Stock Market Ticker 📈

### Scenario
You are developing a real-time market data streaming service for a financial institution. When market tickers update stock prices, multiple downstream sub-systems must react instantaneously:
1. **Portfolio Auto-Trader Bot:** Monitors stock movements and executes automatic `BUY` orders when price drops below a target threshold or `SELL` orders when taking profit above a high target.
2. **Push Notification Service:** Dispatches high-priority email notifications to subscribed investors when price volatility exceeds 5%.
3. **Audit Log System:** Records an immutable audit log of every price change for financial compliance tracking.

---

### What it teaches:
* **The Observer Design Pattern:** Establishing a one-to-many publish-subscribe relationship.
* **Loose Coupling:** The `MarketTicker` publisher does not know concrete subscriber implementation details.
* **Thread-Safe Event Broadcasting & Unsubscription:** Safely attaching/detaching subscribers without corrupting state.

---

### Core Requirements
1. **Domain Models:**
   - `StockPriceUpdate`: Stock ticker symbol, old price, new price, percentage change, timestamp.
2. **Observer Contract (`IMarketObserver`):**
   - `ObserverName` (property)
   - `OnPriceChanged(StockPriceUpdate update)` -> processes tick.
3. **Publisher (`MarketTicker`):**
   - Stores active stock symbols and current prices.
   - Maintains list of `IMarketObserver` subscribers.
   - Provides thread-safe `Subscribe(IMarketObserver)` and `Unsubscribe(IMarketObserver)`.
   - `UpdateStockPrice(string symbol, decimal newPrice)` triggers notifications to all subscribers.
4. **Concrete Observers:**
   - `AutoTraderBot`: Evaluates buy/sell triggers.
   - `InvestorNotifier`: Sends user push/email alerts.
   - `FinancialAuditLogger`: Appends immutable audit traces.
