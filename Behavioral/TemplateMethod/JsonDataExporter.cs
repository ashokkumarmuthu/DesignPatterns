using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Behavioral.TemplateMethod;

/// <summary>
/// Concrete exporter: Transforms data into JSON format.
/// Demonstrates that each exporter plugs into the same pipeline skeleton
/// but produces completely different output formats.
/// </summary>
public class JsonDataExporter : DataExportPipeline
{
    public JsonDataExporter() : base("JSON Exporter") { }

    protected override string TransformData(List<ReportRecord> records)
    {
        var sb = new StringBuilder();
        sb.AppendLine("[");
        for (int i = 0; i < records.Count; i++)
        {
            var r = records[i];
            sb.Append($"  {{ \"id\": \"{r.Id}\", \"name\": \"{r.Name}\", \"category\": \"{r.Category}\", ");
            sb.Append($"\"value\": {r.Value}, \"timestamp\": \"{r.Timestamp:yyyy-MM-dd}\" }}");
            if (i < records.Count - 1) sb.Append(",");
            sb.AppendLine();
        }
        sb.AppendLine("]");
        Console.WriteLine($"  [{ExporterName}] Serialized {records.Count} records to JSON array.");
        return sb.ToString();
    }

    protected override void WriteOutput(string transformedData)
    {
        Console.WriteLine($"  [{ExporterName}] Writing JSON output:");
        Console.WriteLine(transformedData);
    }
}
