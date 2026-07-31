using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Observer;

public class MarketTicker
{
    private readonly Dictionary<string, decimal> _prices = new();
    private readonly List<IMarketObserver> _observers = new();
    private readonly object _lock = new();

    public void RegisterStock(string symbol, decimal initialPrice)
    {
        _prices[symbol] = initialPrice;
    }

    public void Subscribe(IMarketObserver observer)
    {
        lock (_lock)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"  [Ticker] Subscribed -> '{observer.ObserverName}'");
            }
        }
    }

    public void Unsubscribe(IMarketObserver observer)
    {
        lock (_lock)
        {
            if (_observers.Remove(observer))
            {
                Console.WriteLine($"  [Ticker] Unsubscribed -> '{observer.ObserverName}'");
            }
        }
    }

    public void UpdatePrice(string symbol, decimal newPrice)
    {
        if (!_prices.TryGetValue(symbol, out decimal oldPrice))
        {
            Console.WriteLine($"[Error] Stock symbol {symbol} is not tracked.");
            return;
        }

        if (oldPrice == newPrice) return;

        double changePercent = (double)((newPrice - oldPrice) / oldPrice * 100);
        _prices[symbol] = newPrice;

        var update = new StockPriceUpdate(symbol, oldPrice, newPrice, changePercent, DateTime.Now);

        Console.WriteLine($"\n📈 [MARKET TICK] {symbol}: {oldPrice:C} ➔ {newPrice:C} ({changePercent:+0.00;-0.00}%)");

        List<IMarketObserver> snapshot;
        lock (_lock)
        {
            snapshot = new List<IMarketObserver>(_observers);
        }

        foreach (var observer in snapshot)
        {
            observer.OnPriceChanged(update);
        }
    }
}
