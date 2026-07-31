using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Behavioral.TemplateMethod;

/// <summary>
/// Concrete exporter: Transforms data into a formatted PDF-style text report.
/// Also overrides Connect() and Cleanup() hooks to demonstrate that optional
/// steps can be customized without touching the template method itself.
/// </summary>
public class PdfReportExporter : DataExportPipeline
{
    public PdfReportExporter() : base("PDF Report Exporter") { }

    protected override void Connect()
    {
        // Override the hook to add PDF-specific initialization
        Console.WriteLine($"  [{ExporterName}] Initializing PDF rendering engine...");
        base.Connect();
    }

    protected override string TransformData(List<ReportRecord> records)
    {
        var sb = new StringBuilder();
        sb.AppendLine("╔══════════════════════════════════════════════════════════════╗");
        sb.AppendLine("║              MONTHLY SALES REPORT                           ║");
        sb.AppendLine("╠══════════════════════════════════════════════════════════════╣");
        sb.AppendLine("║  ID        │ Product              │ Category      │ Value   ║");
        sb.AppendLine("╠══════════════════════════════════════════════════════════════╣");

        decimal total = 0;
        foreach (var r in records)
        {
            sb.AppendLine($"║  {r.Id,-9} │ {r.Name,-20} │ {r.Category,-13} │ {r.Value,7:F2} ║");
            total += r.Value;
        }

        sb.AppendLine("╠══════════════════════════════════════════════════════════════╣");
        sb.AppendLine($"║  TOTAL: {total,52:F2} ║");
        sb.AppendLine("╚══════════════════════════════════════════════════════════════╝");

        Console.WriteLine($"  [{ExporterName}] Formatted {records.Count} records into PDF table layout.");
        return sb.ToString();
    }

    protected override void WriteOutput(string transformedData)
    {
        Console.WriteLine($"  [{ExporterName}] Rendering PDF document:");
        Console.WriteLine(transformedData);
    }

    protected override void Cleanup()
    {
        Console.WriteLine($"  [{ExporterName}] Flushed PDF buffer and released rendering engine.");
        base.Cleanup();
    }
}
