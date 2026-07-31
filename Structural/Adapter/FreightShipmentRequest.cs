namespace DesignPatterns.Structural.Adapter;

public record FreightShipmentRequest(string TrackingId, string Origin, string Destination, double WeightKg);
