using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Observer;

public class FinancialAuditLogger : IMarketObserver
{
    public string ObserverName => "ComplianceAuditLogger";
    private readonly List<string> _auditRecords = new();

    public void OnPriceChanged(StockPriceUpdate update)
    {
        string record = $"[{update.Timestamp:HH:mm:ss}] AUDIT TAPE: {update.Symbol} {update.OldPrice:C} -> {update.NewPrice:C} ({update.ChangePercent:F2}%)";
        _auditRecords.Add(record);
        Console.WriteLine($"  📜 [{ObserverName}] Logged record #{_auditRecords.Count}");
    }
}
