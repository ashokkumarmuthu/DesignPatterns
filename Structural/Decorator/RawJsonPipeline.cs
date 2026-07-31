namespace DesignPatterns.Structural.Decorator;

public class RawJsonPipeline : IApiResponsePipeline
{
    public string ProcessResponse(string rawJsonPayload)
    {
        return rawJsonPayload;
    }
}
