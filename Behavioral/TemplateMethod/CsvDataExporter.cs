using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Behavioral.TemplateMethod;

/// <summary>
/// Concrete exporter: Transforms data into CSV format and writes to file.
/// Overrides TransformData and WriteOutput while reusing Connect, QueryData, and Cleanup.
/// </summary>
public class CsvDataExporter : DataExportPipeline
{
    public CsvDataExporter() : base("CSV Exporter") { }

    protected override string TransformData(List<ReportRecord> records)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Name,Category,Value,Timestamp");
        foreach (var r in records)
        {
            sb.AppendLine($"{r.Id},{r.Name},{r.Category},{r.Value},{r.Timestamp:yyyy-MM-dd}");
        }
        Console.WriteLine($"  [{ExporterName}] Transformed {records.Count} records into CSV rows.");
        return sb.ToString();
    }

    protected override void WriteOutput(string transformedData)
    {
        Console.WriteLine($"  [{ExporterName}] Writing CSV output:");
        Console.WriteLine(transformedData);
    }
}
