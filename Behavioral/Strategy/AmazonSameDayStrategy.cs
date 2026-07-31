using System;

namespace DesignPatterns.Behavioral.Strategy;

public class AmazonSameDayStrategy : IShippingStrategy
{
    public string CarrierName => "Amazon Same-Day Delivery";

    public ShippingQuote CalculateQuote(ShipmentOrder order)
    {
        decimal total;
        string details;

        if (order.IsPrimeMember && order.OrderValue >= 50.00m)
        {
            total = 0.00m;
            details = "Prime Member Perk (Free Priority Shipping on orders >= $50)";
        }
        else if (order.WeightKg <= 5.0)
        {
            total = 9.99m;
            details = "Flat Rate Standard Parcel (< 5kg)";
        }
        else
        {
            total = 12.50m + (decimal)((order.WeightKg - 5.0) * 2.00);
            details = "Overweight Rate ($12.50 + $2/kg over 5kg)";
        }

        return new ShippingQuote(CarrierName, Math.Round(total, 2), 1, details);
    }
}
