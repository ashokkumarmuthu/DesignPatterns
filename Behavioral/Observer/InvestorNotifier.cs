using System;

namespace DesignPatterns.Behavioral.Observer;

public class InvestorNotifier(string investorName, string email) : IMarketObserver
{
    public string ObserverName => $"InvestorNotifier ({investorName})";
    private readonly string _email = email;

    public void OnPriceChanged(StockPriceUpdate update)
    {
        if (Math.Abs(update.ChangePercent) >= 3.0)
        {
            Console.WriteLine($"  📧 [{ObserverName}] High Volatility Alert sent to {_email}: {update.Symbol} moved by {update.ChangePercent:+0.00;-0.00}%!");
        }
    }
}
