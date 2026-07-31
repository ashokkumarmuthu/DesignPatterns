namespace DesignPatterns.Structural.Adapter;

public interface IFreightProvider
{
    string ProviderName { get; }
    FreightStatusUpdate DispatchFreight(FreightShipmentRequest request);
}
