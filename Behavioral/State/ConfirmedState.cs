using System;

namespace DesignPatterns.Behavioral.State;

public class ConfirmedState : IOrderState
{
    public string StatusName => "Confirmed";

    public void Confirm(Order order)
    {
        Console.WriteLine($"  ❌ Order #{order.OrderId} is already confirmed.");
    }

    public void Ship(Order order)
    {
        Console.WriteLine($"  🚚 Order #{order.OrderId} handed to courier. Tracking number generated.");
        order.TransitionTo(new ShippedState());
    }

    public void Deliver(Order order)
    {
        Console.WriteLine($"  ❌ Cannot deliver Order #{order.OrderId} — it hasn't been shipped yet.");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine($"  🚫 Order #{order.OrderId} cancelled before shipping. Refund of {order.TotalAmount:C} initiated.");
        order.TransitionTo(new CancelledState());
    }
}
