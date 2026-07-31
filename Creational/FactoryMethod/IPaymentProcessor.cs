namespace DesignPatterns.Creational.FactoryMethod;

public interface IPaymentProcessor
{
    string ProviderName { get; }
    PaymentResult ExecutePayment(PaymentRequest request);
}
