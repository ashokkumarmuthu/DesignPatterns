using System;

namespace DesignPatterns.Behavioral.Observer;

public record StockPriceUpdate(
    string Symbol,
    decimal OldPrice,
    decimal NewPrice,
    double ChangePercent,
    DateTime Timestamp
);
