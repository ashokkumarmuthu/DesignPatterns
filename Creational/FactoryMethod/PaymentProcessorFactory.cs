using System;

namespace DesignPatterns.Creational.FactoryMethod;

public abstract class PaymentProcessorFactory
{
    public abstract IPaymentProcessor CreateProcessor();

    public PaymentResult Process(PaymentRequest request)
    {
        Console.WriteLine($"\n💳 Initiating Payment Processing for Transaction #{request.TransactionId}");
        IPaymentProcessor processor = CreateProcessor();
        Console.WriteLine($"  Selected Provider: {processor.ProviderName}");
        return processor.ExecutePayment(request);
    }
}
