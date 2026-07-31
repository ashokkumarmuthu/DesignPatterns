using System;

namespace DesignPatterns.Behavioral.State;

public class ShippedState : IOrderState
{
    public string StatusName => "Shipped";

    public void Confirm(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is already confirmed and shipped.");
    }

    public void Ship(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is already in transit.");
    }

    public void Deliver(Order order)
    {
        Console.WriteLine($"  📬 Order #{order.OrderId} delivered successfully to {order.CustomerName}.");
        order.TransitionTo(new DeliveredState());
    }

    public void Cancel(Order order)
    {
        Console.WriteLine($"  ❌ Cannot cancel Order #{order.OrderId} — it's already shipped and in transit.");
    }
}
