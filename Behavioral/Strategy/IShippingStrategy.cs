namespace DesignPatterns.Behavioral.Strategy;

public interface IShippingStrategy
{
    string CarrierName { get; }
    ShippingQuote CalculateQuote(ShipmentOrder order);
}
