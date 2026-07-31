using System;

namespace DesignPatterns.Behavioral.Strategy;

// Problem Solved: Dynamic Shipping Cost Calculation Strategy
// 1. The Strategy Interface — contract for all shipping algorithms
public interface IShippingCalculator
{
    string Name { get; }
    decimal Calculate(double weightKg);
}

// 2. Concrete Strategy A: Standard Shipping ($1.50 per kg)
public class StandardShipping : IShippingCalculator
{
    public string Name => "Standard (5-7 days)";
    public decimal Calculate(double weightKg) => (decimal)(weightKg * 1.50);
}

// 3. Concrete Strategy B: Express Shipping ($3.00 per kg + $5 surcharge)
public class ExpressShipping : IShippingCalculator
{
    public string Name => "Express (2-3 days)";
    public decimal Calculate(double weightKg) => (decimal)(weightKg * 3.00) + 5.00m;
}

// 4. Concrete Strategy C: Overnight Shipping ($5.00 per kg + $15 rush fee)
public class OvernightShipping : IShippingCalculator
{
    public string Name => "Overnight (next day)";
    public decimal Calculate(double weightKg) => (decimal)(weightKg * 5.00) + 15.00m;
}

// 5. The Context — uses the strategy without knowing concrete details
public class OrderService
{
    private IShippingCalculator _shippingCalculator;

    public OrderService(IShippingCalculator shippingCalculator)
    {
        _shippingCalculator = shippingCalculator;
    }

    public void SetShippingMethod(IShippingCalculator shippingCalculator)
    {
        _shippingCalculator = shippingCalculator;
    }

    public void Checkout(string product, double weightKg)
    {
        decimal shippingCost = _shippingCalculator.Calculate(weightKg);
        Console.WriteLine($"Product: {product} | Weight: {weightKg}kg | Shipping: {_shippingCalculator.Name} | Cost: {shippingCost:C}");
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== STRATEGY PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Swapping shipping rate algorithms at runtime\n");

        var order = new OrderService(new StandardShipping());
        order.Checkout("Laptop", 2.5);

        // Swap strategy at runtime
        order.SetShippingMethod(new ExpressShipping());
        order.Checkout("Laptop", 2.5);

        order.SetShippingMethod(new OvernightShipping());
        order.Checkout("Laptop", 2.5);

        Console.WriteLine("=============================\n");
    }
}
