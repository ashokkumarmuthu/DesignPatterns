using System;

namespace DesignPatterns.Behavioral.Observer;

public class AutoTraderBot(string botId, string targetSymbol, decimal buyThreshold, decimal sellThreshold) : IMarketObserver
{
    public string ObserverName => $"AutoTraderBot ({botId})";
    private readonly string _targetSymbol = targetSymbol;
    private readonly decimal _buyThreshold = buyThreshold;
    private readonly decimal _sellThreshold = sellThreshold;

    public void OnPriceChanged(StockPriceUpdate update)
    {
        if (update.Symbol != _targetSymbol) return;

        if (update.NewPrice <= _buyThreshold)
        {
            Console.WriteLine($"  🤖 [{ObserverName}] TRIGGER AUTO-BUY: {update.Symbol} fell to {update.NewPrice:C} (Target: <={_buyThreshold:C})");
        }
        else if (update.NewPrice >= _sellThreshold)
        {
            Console.WriteLine($"  🤖 [{ObserverName}] TRIGGER AUTO-SELL: {update.Symbol} rose to {update.NewPrice:C} (Target: >= {_sellThreshold:C})");
        }
    }
}
