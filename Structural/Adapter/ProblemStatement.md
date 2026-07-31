# Adapter Pattern Challenge: Third-Party Enterprise Logistics & Freight Adapter 🚚

### Scenario
Your company operates an enterprise Supply Chain & Freight Management system. Your core system expects all logistics vendors to implement your standard JSON REST interface `IFreightProvider`:
```csharp
public record FreightShipmentRequest(string TrackingId, string Origin, string Destination, double WeightKg);
public record FreightStatusUpdate(string TrackingId, string Status, int EstimatedHours);

public interface IFreightProvider {
    FreightStatusUpdate DispatchFreight(FreightShipmentRequest request);
}
```

You are onboarding an international ocean cargo carrier, **LegacyMaritimeLogistics**, which provides a 20-year-old SOAP/XML-based legacy C# SDK (`LegacySoapFreightClient`) that cannot be modified:
```csharp
public class LegacySoapFreightClient {
    public int ExecuteXMLShipment(string xmlPayload) { ... }
}
```
`LegacySoapFreightClient` accepts formatted XML strings (`<Shipment><ID>...</ID>...</Shipment>`) and returns an integer status code (`1` = Dispatched, `0` = Pending, `-1` = Error).

---

### What it teaches:
* **The Adapter Design Pattern:** Reconciling incompatible third-party or legacy interfaces with a modern target interface.
* **Data & Protocol Transformation:** Converting JSON domain models into XML envelopes and back.

---

### Core Requirements
1. **Target Interface (`IFreightProvider`).**
2. **Incompatible Legacy Class (`LegacySoapFreightClient`).**
3. **Adapter Class (`LegacyMaritimeLogisticsAdapter`):** Implements `IFreightProvider`, serializes domain requests to XML, calls legacy SDK, parses response codes into `FreightStatusUpdate`.
