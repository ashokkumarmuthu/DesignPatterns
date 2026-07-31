using System;
using System.Diagnostics;

namespace DesignPatterns.Structural.Decorator;

public class TelemetryPipelineDecorator(IApiResponsePipeline inner, string endpoint) : PipelineDecorator(inner)
{
    private readonly string _endpoint = endpoint;

    public override string ProcessResponse(string rawJsonPayload)
    {
        Console.WriteLine($"  ⏱️ [Telemetry Layer] Starting pipeline execution for endpoint '{_endpoint}'...");
        var sw = Stopwatch.StartNew();
        string output = base.ProcessResponse(rawJsonPayload);
        sw.Stop();
        Console.WriteLine($"  ⏱️ [Telemetry Layer] Endpoint '{_endpoint}' completed in {sw.Elapsed.TotalMilliseconds:F3}ms");
        return output;
    }
}
