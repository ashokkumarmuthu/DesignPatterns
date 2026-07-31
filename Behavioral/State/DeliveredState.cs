using System;

namespace DesignPatterns.Behavioral.State;

public class DeliveredState : IOrderState
{
    public string StatusName => "Delivered";

    public void Confirm(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} has already been delivered. No further actions allowed.");
    }

    public void Ship(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} has already been delivered. No further actions allowed.");
    }

    public void Deliver(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} has already been delivered. No further actions allowed.");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine($"  ❌ Cannot cancel Order #{order.OrderId} — it has already been delivered. Please initiate a return instead.");
    }
}
