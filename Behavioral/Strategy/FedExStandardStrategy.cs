using System;

namespace DesignPatterns.Behavioral.Strategy;

public class FedExStandardStrategy : IShippingStrategy
{
    public string CarrierName => "FedEx Standard Ground";

    public ShippingQuote CalculateQuote(ShipmentOrder order)
    {
        decimal baseCost = 5.00m;
        decimal weightCost = (decimal)(order.WeightKg * 1.50);
        decimal distanceCost = (decimal)(order.DistanceMiles * 0.05);
        decimal total = baseCost + weightCost + distanceCost;
        int days = order.DistanceMiles > 1000 ? 5 : 3;

        string details = $"Base: {baseCost:C} + Weight ({order.WeightKg}kg): {weightCost:C} + Distance ({order.DistanceMiles}mi): {distanceCost:C}";
        return new ShippingQuote(CarrierName, Math.Round(total, 2), days, details);
    }
}
