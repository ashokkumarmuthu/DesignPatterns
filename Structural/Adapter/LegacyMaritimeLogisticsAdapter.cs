namespace DesignPatterns.Structural.Adapter;

public class LegacyMaritimeLogisticsAdapter(LegacySoapFreightClient legacyClient) : IFreightProvider
{
    public string ProviderName => "Legacy Maritime Ocean Cargo (SOAP/XML Adapter)";
    private readonly LegacySoapFreightClient _legacyClient = legacyClient;

    public FreightStatusUpdate DispatchFreight(FreightShipmentRequest request)
    {
        string xmlPayload = $"""
            <SoapEnvelope>
              <ShipmentHeader ID="{request.TrackingId}" />
              <Route From="{request.Origin}" To="{request.Destination}" />
              <Cargo WeightKg="{request.WeightKg}" />
            </SoapEnvelope>
            """;

        int legacyReturnCode = _legacyClient.ExecuteXMLShipment(xmlPayload);

        string status = legacyReturnCode switch
        {
            1 => "DISPATCHED_OVERSEAS",
            0 => "PENDING_CUSTOMS",
            _ => "SHIPMENT_FAILED"
        };

        int estHours = request.WeightKg > 500 ? 120 : 72;
        return new FreightStatusUpdate(request.TrackingId, status, estHours);
    }
}
