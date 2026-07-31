namespace DesignPatterns.Creational.FactoryMethod;

public class PayPalProcessorFactory(string clientId, string clientSecret) : PaymentProcessorFactory
{
    private readonly string _clientId = clientId;
    private readonly string _clientSecret = clientSecret;
    public override IPaymentProcessor CreateProcessor() => new PayPalPaymentProcessor(_clientId, _clientSecret);
}
