using System;

namespace DesignPatterns.Behavioral.State;

/// <summary>
/// State interface defining all possible actions on an order.
/// Each concrete state determines which actions are valid and which are rejected.
/// </summary>
public interface IOrderState
{
    string StatusName { get; }
    void Confirm(Order order);
    void Ship(Order order);
    void Deliver(Order order);
    void Cancel(Order order);
}
