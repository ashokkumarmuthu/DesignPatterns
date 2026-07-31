# Template Method Pattern Challenge: Enterprise Data Export Pipeline 📊

### Scenario
You are building a reporting module for an enterprise analytics platform. The system must export data to multiple formats: **CSV** (for Excel users), **JSON** (for API consumers), and **PDF** (for executive summaries).

Every export follows the **same 5-step pipeline**:
1. **Connect** to the data source
2. **Query** raw records
3. **Transform** records into the target format
4. **Write** the formatted output
5. **Cleanup** resources and connections

The pipeline structure is invariant — but steps 3 and 4 differ drastically between formats. Adding a new format (e.g., XML, Parquet) should only require implementing the format-specific steps, not re-implementing the entire pipeline.

---

### What it teaches:
* **The Template Method Pattern:** Defining an algorithm skeleton in a base class with customizable steps in subclasses.
* **Hollywood Principle ("Don't call us, we'll call you"):** The base class controls the flow and calls subclass methods at the right time.
* **Hook Methods:** Optional override points (Connect, Cleanup) that provide default behavior but allow customization.

---

### Core Requirements
1. **Abstract Base Class (`DataExportPipeline`):**
   - Template method: `Export()` — calls all 5 steps in fixed order
   - Abstract steps: `TransformData()`, `WriteOutput()`
   - Virtual hooks: `Connect()`, `QueryData()`, `Cleanup()`
2. **Concrete Exporters:**
   - `CsvDataExporter` — comma-separated flat file output
   - `JsonDataExporter` — JSON array serialization
   - `PdfReportExporter` — formatted table with totals, custom Connect/Cleanup hooks
3. **Domain Model:**
   - `ReportRecord`: Id, Name, Category, Value, Timestamp
