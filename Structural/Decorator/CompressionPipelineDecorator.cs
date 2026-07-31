using System;

namespace DesignPatterns.Structural.Decorator;

public class CompressionPipelineDecorator(IApiResponsePipeline inner) : PipelineDecorator(inner)
{
    public override string ProcessResponse(string rawJsonPayload)
    {
        string input = base.ProcessResponse(rawJsonPayload);
        string compressed = $"[GZIP_COMPRESSED_DATA: length={input.Length} -> {input.Length / 2} bytes: ({input[..Math.Min(20, input.Length)]}...)]";
        Console.WriteLine("  📦 [Compression Layer] Applied GZip compression (50% reduction)");
        return compressed;
    }
}
