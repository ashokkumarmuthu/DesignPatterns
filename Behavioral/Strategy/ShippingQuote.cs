namespace DesignPatterns.Behavioral.Strategy;

public record ShippingQuote(
    string CarrierName,
    decimal TotalCost,
    int EstimatedDays,
    string BreakdownDetails
);
