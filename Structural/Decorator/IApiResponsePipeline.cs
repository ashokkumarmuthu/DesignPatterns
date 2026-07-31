namespace DesignPatterns.Structural.Decorator;

public interface IApiResponsePipeline
{
    string ProcessResponse(string rawJsonPayload);
}
