using System;

namespace DesignPatterns.Structural.Adapter;

public class ModernRestFreightProvider : IFreightProvider
{
    public string ProviderName => "FedEx Freight API (REST/JSON)";

    public FreightStatusUpdate DispatchFreight(FreightShipmentRequest request)
    {
        Console.WriteLine($"  [Modern REST Client] POST /v2/shipments -> Tracking #{request.TrackingId}");
        return new FreightStatusUpdate(request.TrackingId, "IN_TRANSIT", 48);
    }
}
