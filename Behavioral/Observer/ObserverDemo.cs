using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Observer;

// Problem Solved: Real-Time Multi-Subscriber Stock Market Notification
// 1. Observer Interface
public interface IObserver
{
    void Update(string stockSymbol, decimal price);
}

// 2. Subject (Publisher)
public class Stock
{
    public string Symbol { get; }
    private decimal _price;
    private readonly List<IObserver> _observers = new();

    public Stock(string symbol, decimal initialPrice)
    {
        Symbol = symbol;
        _price = initialPrice;
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine($"  [Subscribed] New observer attached to {Symbol}.");
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"  [Unsubscribed] Observer detached from {Symbol}.");
    }

    public void SetPrice(decimal newPrice)
    {
        decimal oldPrice = _price;
        _price = newPrice;
        Console.WriteLine($"\n[{Symbol}] Price changed: {oldPrice:C} -> {newPrice:C}");
        NotifyAll();
    }

    private void NotifyAll()
    {
        foreach (var observer in _observers)
        {
            observer.Update(Symbol, _price);
        }
    }
}

// 3. Concrete Observer A: Investor
public class Investor : IObserver
{
    public string Name { get; }
    public Investor(string name) { Name = name; }

    public void Update(string stockSymbol, decimal price)
    {
        Console.WriteLine($"  [{Name}] Notified: {stockSymbol} is now {price:C}.");
    }
}

// 4. Concrete Observer B: Automated Trading Bot
public class TradingBot : IObserver
{
    private readonly decimal _buyThreshold;
    public TradingBot(decimal buyThreshold) { _buyThreshold = buyThreshold; }

    public void Update(string stockSymbol, decimal price)
    {
        if (price <= _buyThreshold)
            Console.WriteLine($"  [TradingBot] AUTO-BUY triggered for {stockSymbol} at {price:C} (threshold: {_buyThreshold:C}).");
        else
            Console.WriteLine($"  [TradingBot] Watching {stockSymbol}... price {price:C} is above threshold {_buyThreshold:C}.");
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== OBSERVER PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Decoupled live stock notifications to multiple listeners\n");

        var appleStock = new Stock("AAPL", 150.00m);

        var investor1 = new Investor("Ashok");
        var investor2 = new Investor("Kumar");
        var bot = new TradingBot(140.00m);

        appleStock.Attach(investor1);
        appleStock.Attach(investor2);
        appleStock.Attach(bot);

        appleStock.SetPrice(155.00m);
        appleStock.SetPrice(138.00m);

        appleStock.Detach(investor2);
        appleStock.SetPrice(142.00m);

        Console.WriteLine("=============================\n");
    }
}
