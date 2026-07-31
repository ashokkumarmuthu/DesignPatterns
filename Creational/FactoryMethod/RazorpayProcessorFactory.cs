namespace DesignPatterns.Creational.FactoryMethod;

public class RazorpayProcessorFactory(string keyId) : PaymentProcessorFactory
{
    private readonly string _keyId = keyId;
    public override IPaymentProcessor CreateProcessor() => new RazorpayPaymentProcessor(_keyId);
}
