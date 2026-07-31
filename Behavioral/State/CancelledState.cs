using System;

namespace DesignPatterns.Behavioral.State;

public class CancelledState : IOrderState
{
    public string StatusName => "Cancelled";

    public void Confirm(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is cancelled and cannot be confirmed.");
    }

    public void Ship(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is cancelled and cannot be shipped.");
    }

    public void Deliver(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is cancelled and cannot be delivered.");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is already cancelled.");
    }
}
