namespace DesignPatterns.Structural.Decorator;

public abstract class PipelineDecorator(IApiResponsePipeline inner) : IApiResponsePipeline
{
    protected readonly IApiResponsePipeline _inner = inner;

    public virtual string ProcessResponse(string rawJsonPayload)
    {
        return _inner.ProcessResponse(rawJsonPayload);
    }
}
