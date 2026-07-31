# Singleton Pattern Challenge: Enterprise App Configuration & Cache Manager 🔒

### Scenario
You are developing a high-throughput backend Web API microservice. The application relies on a shared `AppConfigurationManager` that reads environment variables, database connection strings, API rate limits, and feature toggles.

If multiple services instantiate their own copy of configuration files:
1. **Memory & I/O Waste:** Disk reads and JSON parsing are executed repeatedly across hundreds of worker threads.
2. **Race Conditions:** Thread-unsafe configuration updates lead to corrupted configuration states during runtime reloads.

---

### What it teaches:
* **The Singleton Design Pattern:** Restricting instantiation to exactly one shared instance across the entire application domain.
* **Thread Safety & Lazy Initialization:** Guaranteeing double-check locking or `Lazy<T>` safety under multi-threaded concurrency.
* **In-Memory Thread-Safe Caching:** Managing synchronized key-value dictionary caches.

---

### Core Requirements
1. **Thread-Safe Singleton (`AppConfigurationManager`):**
   - Implements thread-safe singleton lifecycle.
   - Loads configuration settings (`Environment`, `DatabaseConnectionString`, `MaxRetryCount`, `FeatureToggles`).
   - Implements thread-safe `GetSetting(key)` and `SetSetting(key, value)`.
   - `ReloadConfiguration()` re-reads simulated settings atomically.
2. **Concurrent Multithread Test:**
   - Spawns 10 parallel tasks attempting to fetch configurations simultaneously to prove single instance identity.
