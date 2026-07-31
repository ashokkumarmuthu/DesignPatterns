namespace DesignPatterns.Behavioral.Strategy;

public record ShipmentOrder(
    string OrderId,
    double WeightKg,
    double DistanceMiles,
    string DestinationCountry,
    decimal OrderValue,
    bool IsPrimeMember
);
