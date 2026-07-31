using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.TemplateMethod;

// ============================================================
// Domain Model
// ============================================================

public record ReportRecord(string Id, string Name, string Category, decimal Value, DateTime Timestamp);

// ============================================================
// THE TEMPLATE METHOD — Abstract base class defining the algorithm skeleton
// ============================================================

/// <summary>
/// Defines the invariant pipeline: Connect → Query → Transform → Write → Cleanup.
/// Subclasses override specific steps (Transform, Write) without changing the overall flow.
/// </summary>
public abstract class DataExportPipeline
{
    public string ExporterName { get; }

    protected DataExportPipeline(string exporterName)
    {
        ExporterName = exporterName;
    }

    /// <summary>
    /// TEMPLATE METHOD — The fixed algorithm skeleton. Cannot be overridden.
    /// Calls each step in the guaranteed order.
    /// </summary>
    public void Export()
    {
        Console.WriteLine($"\n  [{ExporterName}] Starting export pipeline...");
        Connect();
        var rawData = QueryData();
        Console.WriteLine($"  [{ExporterName}] Queried {rawData.Count} records.");
        var transformedData = TransformData(rawData);
        WriteOutput(transformedData);
        Cleanup();
        Console.WriteLine($"  [{ExporterName}] Export complete.\n");
    }

    // Step 1: Connect to data source (default implementation, can be overridden)
    protected virtual void Connect()
    {
        Console.WriteLine($"  [{ExporterName}] Connected to default SQL data source.");
    }

    // Step 2: Query raw data (shared across all exporters)
    protected virtual List<ReportRecord> QueryData()
    {
        return new List<ReportRecord>
        {
            new("RPT-001", "Widget Alpha", "Electronics", 149.99m, DateTime.Now.AddDays(-3)),
            new("RPT-002", "Gadget Beta", "Electronics", 299.50m, DateTime.Now.AddDays(-1)),
            new("RPT-003", "Service Plan Pro", "Subscriptions", 49.99m, DateTime.Now),
            new("RPT-004", "Cable Adapter X", "Accessories", 12.75m, DateTime.Now.AddDays(-7)),
        };
    }

    // Step 3: Transform data — MUST be overridden by each exporter
    protected abstract string TransformData(List<ReportRecord> records);

    // Step 4: Write output — MUST be overridden by each exporter
    protected abstract void WriteOutput(string transformedData);

    // Step 5: Cleanup (hook — optional override)
    protected virtual void Cleanup()
    {
        Console.WriteLine($"  [{ExporterName}] Closed data source connection.");
    }
}
