using System;

namespace DesignPatterns.Structural.Adapter;

public class LegacySoapFreightClient
{
    public int ExecuteXMLShipment(string xmlPayload)
    {
        Console.WriteLine($"  [Legacy SOAP SDK] Dispatching raw XML Envelope:\n{xmlPayload}");
        return 1; // 1 = Dispatched Success
    }
}
