# Template Method Pattern 📊

> *"Define the skeleton of an algorithm in an operation, deferring some steps to subclasses."*

---

## 📋 Problem Statement
**Scenario:** A data export system must support CSV, JSON, and PDF output. Every export follows the same 5-step pipeline (Connect → Query → Transform → Write → Cleanup), but the Transform and Write steps differ per format.

### The Naive / Bad Approach (Without Template Method):
```csharp
public void ExportCsv() { Connect(); Query(); TransformToCsv(); WriteCsvFile(); Cleanup(); }
public void ExportJson() { Connect(); Query(); TransformToJson(); WriteJsonFile(); Cleanup(); }
public void ExportPdf() { Connect(); Query(); TransformToPdf(); WritePdfFile(); Cleanup(); }
// Every new format duplicates the entire pipeline flow!
```
**Why this breaks:**
- Pipeline logic (ordering, error handling) is duplicated across every format.
- Changing the pipeline structure (e.g., adding a "Validate" step) requires editing every exporter.
- Common steps (Connect, Cleanup) are copy-pasted and drift apart over time.

---

## 💡 How Template Method Solves the Problem
1. **`DataExportPipeline` (Abstract Base Class):** Defines the fixed `Export()` method that calls all 5 steps in order. This method is NOT overridable.
2. **Abstract Steps (`TransformData`, `WriteOutput`):** Each subclass MUST implement these format-specific steps.
3. **Virtual Hooks (`Connect`, `Cleanup`):** Provide sensible defaults but allow subclasses to customize if needed (e.g., PDF initializes a rendering engine).

---

## ⚖️ Trade-offs & Advantages
- **Pros:** Eliminates pipeline duplication; enforces consistent step ordering; adding new formats only requires implementing 2 methods.
- **Cons:** Relies on inheritance (less flexible than composition); can become complex with many hooks; debugging requires tracing through base + subclass.

---

## 💻 C# Implementation Details
See [DataExportPipeline.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/TemplateMethod/DataExportPipeline.cs) for the abstract base class with the template method, and [CsvDataExporter.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/TemplateMethod/CsvDataExporter.cs), [JsonDataExporter.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/TemplateMethod/JsonDataExporter.cs), [PdfReportExporter.cs](file:///Users/ashokkumar/Desktop/Dotnet/DotnetPractice/DesignPatterns/Behavioral/TemplateMethod/PdfReportExporter.cs) for the concrete implementations.
