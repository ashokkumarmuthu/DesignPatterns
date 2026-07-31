namespace DesignPatterns.Behavioral.Observer;

public interface IMarketObserver
{
    string ObserverName { get; }
    void OnPriceChanged(StockPriceUpdate update);
}
