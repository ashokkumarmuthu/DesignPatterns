using System;

namespace DesignPatterns.Behavioral.State;

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== STATE PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Clean order lifecycle without nested if/switch state checks\n");

        // --- Happy Path: Pending → Confirmed → Shipped → Delivered ---
        Console.WriteLine("--- Scenario 1: Successful Order Lifecycle ---");
        var order1 = new Order("ORD-5001", "Ashok Kumar", 249.99m);
        order1.PrintStatus();
        order1.Confirm();
        order1.Ship();
        order1.Deliver();
        order1.PrintStatus();

        // --- Try invalid transitions on delivered order ---
        Console.WriteLine("\n--- Scenario 2: Invalid Actions on Delivered Order ---");
        order1.Cancel();   // Should be rejected
        order1.Ship();     // Should be rejected

        // --- Cancel before shipping ---
        Console.WriteLine("\n--- Scenario 3: Cancellation Flow ---");
        var order2 = new Order("ORD-5002", "Priya Sharma", 89.50m);
        order2.Confirm();
        order2.Cancel();   // Should succeed — not yet shipped
        order2.Confirm();  // Should be rejected — already cancelled
        order2.PrintStatus();

        // --- Try to skip states ---
        Console.WriteLine("\n--- Scenario 4: Attempt to Skip States ---");
        var order3 = new Order("ORD-5003", "Raj Patel", 450.00m);
        order3.Ship();     // Should be rejected — not confirmed yet
        order3.Deliver();  // Should be rejected — not shipped yet

        Console.WriteLine("\n==========================\n");
    }
}
