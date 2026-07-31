using System;

namespace DesignPatterns.Behavioral.Strategy;

public class DhlExpressStrategy : IShippingStrategy
{
    public string CarrierName => "DHL Express International";

    public ShippingQuote CalculateQuote(ShipmentOrder order)
    {
        decimal baseCost = 25.00m;
        decimal weightCost = (decimal)(order.WeightKg * 4.00);
        bool isOverseas = order.DestinationCountry != "USA";
        decimal customsTax = isOverseas ? (baseCost + weightCost) * 0.15m : 0m;

        decimal total = baseCost + weightCost + customsTax;
        int days = isOverseas ? 3 : 1;

        string details = $"Base: {baseCost:C} + Weight: {weightCost:C}" + (isOverseas ? $" + Overseas Customs Tax: {customsTax:C}" : "");
        return new ShippingQuote(CarrierName, Math.Round(total, 2), days, details);
    }
}
