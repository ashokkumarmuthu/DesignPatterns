using System;

namespace DesignPatterns.Behavioral.State;

public class PendingState : IOrderState
{
    public string StatusName => "Pending";

    public void Confirm(Order order)
    {
        Console.WriteLine($"  ✅ Order #{order.OrderId} confirmed. Payment verified for {order.TotalAmount:C}.");
        order.TransitionTo(new ConfirmedState());
    }

    public void Ship(Order order)
    {
        Console.WriteLine($"  ❌ Cannot ship Order #{order.OrderId} — it hasn't been confirmed yet.");
    }

    public void Deliver(Order order)
    {
        Console.WriteLine($"  ❌ Cannot deliver Order #{order.OrderId} — it hasn't been shipped yet.");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine($"  🚫 Order #{order.OrderId} cancelled. Full refund of {order.TotalAmount:C} initiated.");
        order.TransitionTo(new CancelledState());
    }
}
