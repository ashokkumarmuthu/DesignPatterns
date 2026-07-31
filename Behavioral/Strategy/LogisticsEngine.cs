using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Behavioral.Strategy;

public class LogisticsEngine
{
    private readonly List<IShippingStrategy> _registeredCarriers = new();

    public void RegisterCarrier(IShippingStrategy carrierStrategy)
    {
        _registeredCarriers.Add(carrierStrategy);
    }

    public ShippingQuote CalculateSingleQuote(IShippingStrategy strategy, ShipmentOrder order)
    {
        return strategy.CalculateQuote(order);
    }

    public List<ShippingQuote> CompareAllQuotes(ShipmentOrder order)
    {
        return _registeredCarriers.Select(carrier => carrier.CalculateQuote(order)).ToList();
    }

    public ShippingQuote FindCheapestQuote(ShipmentOrder order)
    {
        if (_registeredCarriers.Count == 0)
            throw new InvalidOperationException("No carriers registered in the logistics engine.");

        return CompareAllQuotes(order).OrderBy(q => q.TotalCost).First();
    }
}
