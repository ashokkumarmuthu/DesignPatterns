namespace DesignPatterns.Creational.FactoryMethod;

public class StripeProcessorFactory(string apiKey) : PaymentProcessorFactory
{
    private readonly string _apiKey = apiKey;
    public override IPaymentProcessor CreateProcessor() => new StripePaymentProcessor(_apiKey);
}
