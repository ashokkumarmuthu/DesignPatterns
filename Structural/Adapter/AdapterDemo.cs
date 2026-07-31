using System;
using System.Collections.Generic;

namespace DesignPatterns.Structural.Adapter;

// Problem Solved: Bridging Incompatible Legacy Bank SDK to Modern IPaymentGateway Interface
public interface IPaymentGateway
{
    bool ProcessPayment(decimal amount, string currency);
}

public class StripeGateway : IPaymentGateway
{
    public bool ProcessPayment(decimal amount, string currency)
    {
        Console.WriteLine($"  [Stripe] Charged {amount:C} ({currency}) successfully.");
        return true;
    }
}

// Incompatible Third-Party / Legacy SDK
public class LegacyBankSdk
{
    public int MakePayment(double amount, int currencyCode)
    {
        Console.WriteLine($"  [LegacyBank] Processing {amount:F2} with numeric currency code {currencyCode}...");
        return 0; // 0 = Success
    }
}

// The Adapter
public class LegacyBankAdapter : IPaymentGateway
{
    private readonly LegacyBankSdk _legacyBank;

    private static readonly Dictionary<string, int> CurrencyMap = new()
    {
        { "USD", 840 },
        { "EUR", 978 },
        { "GBP", 826 },
        { "INR", 356 },
    };

    public LegacyBankAdapter(LegacyBankSdk legacyBank)
    {
        _legacyBank = legacyBank;
    }

    public bool ProcessPayment(decimal amount, string currency)
    {
        double legacyAmount = (double)amount;
        int currencyCode = CurrencyMap.GetValueOrDefault(currency.ToUpper(), 840);

        int result = _legacyBank.MakePayment(legacyAmount, currencyCode);
        bool success = result == 0;
        Console.WriteLine($"  [Adapter] Translated legacy result code {result} -> {(success ? "Success" : "Failed")}");
        return success;
    }
}

public class CheckoutService
{
    private readonly IPaymentGateway _gateway;
    public CheckoutService(IPaymentGateway gateway) { _gateway = gateway; }

    public void ProcessOrder(string product, decimal price, string currency)
    {
        Console.WriteLine($"  Ordering: {product} for {price:C} ({currency})");
        bool result = _gateway.ProcessPayment(price, currency);
        Console.WriteLine($"  Order status: {(result ? "Completed ✅" : "Failed ❌")}\n");
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== ADAPTER PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Adapting LegacyBankSdk to IPaymentGateway\n");

        Console.WriteLine("--- Native Gateway ---");
        var stripeCheckout = new CheckoutService(new StripeGateway());
        stripeCheckout.ProcessOrder("Laptop", 999.99m, "USD");

        Console.WriteLine("--- Adapted Legacy Gateway ---");
        var legacyBank = new LegacyBankSdk();
        var adapter = new LegacyBankAdapter(legacyBank);
        var legacyCheckout = new CheckoutService(adapter);
        legacyCheckout.ProcessOrder("Smartphone", 699.00m, "INR");

        Console.WriteLine("============================\n");
    }
}
