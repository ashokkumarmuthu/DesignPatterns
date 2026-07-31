using System;

namespace DesignPatterns.Behavioral.TemplateMethod;

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== TEMPLATE METHOD PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Same pipeline skeleton, different export formats\n");

        // All three exporters share the SAME pipeline flow:
        //   Connect → QueryData → TransformData → WriteOutput → Cleanup
        // But each one produces completely different output.

        DataExportPipeline csvExporter = new CsvDataExporter();
        csvExporter.Export();

        DataExportPipeline jsonExporter = new JsonDataExporter();
        jsonExporter.Export();

        DataExportPipeline pdfExporter = new PdfReportExporter();
        pdfExporter.Export();

        Console.WriteLine("====================================\n");
    }
}
