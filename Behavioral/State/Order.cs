using System;

namespace DesignPatterns.Behavioral.State;

/// <summary>
/// The Context — holds the current state and delegates all actions to it.
/// State transitions are performed by the state objects themselves, which
/// call order.TransitionTo() to change the context's state.
/// </summary>
public class Order
{
    public string OrderId { get; }
    public string CustomerName { get; }
    public decimal TotalAmount { get; }
    public IOrderState CurrentState { get; private set; }

    public Order(string orderId, string customerName, decimal totalAmount)
    {
        OrderId = orderId;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        CurrentState = new PendingState();   // All orders start in Pending state
    }

    /// <summary>
    /// Called by state objects to transition the order to a new state.
    /// </summary>
    public void TransitionTo(IOrderState newState)
    {
        Console.WriteLine($"  📦 Order #{OrderId}: [{CurrentState.StatusName}] → [{newState.StatusName}]");
        CurrentState = newState;
    }

    // Delegate all actions to the current state
    public void Confirm() => CurrentState.Confirm(this);
    public void Ship() => CurrentState.Ship(this);
    public void Deliver() => CurrentState.Deliver(this);
    public void Cancel() => CurrentState.Cancel(this);

    public void PrintStatus()
    {
        Console.WriteLine($"  Order #{OrderId} | Customer: {CustomerName} | Amount: {TotalAmount:C} | Status: {CurrentState.StatusName}");
    }
}
