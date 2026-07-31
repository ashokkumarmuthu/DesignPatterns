using System;

namespace DesignPatterns.Creational.FactoryMethod;

public class PayPalPaymentProcessor(string clientId, string clientSecret) : IPaymentProcessor
{
    public string ProviderName => "PayPal Express Checkout";
    private readonly string _clientId = clientId;
    private readonly string _clientSecret = clientSecret;

    public PaymentResult ExecutePayment(PaymentRequest request)
    {
        Console.WriteLine($"  [PayPal SDK] Exchanging OAuth Token for Client ID '{_clientId}'...");
        Console.WriteLine($"  [PayPal SDK] Capturing Express Order {request.TransactionId} ({request.Amount} {request.Currency})...");
        return new PaymentResult(true, $"PAYPAL-PAY-{Guid.NewGuid().ToString()[..8].ToUpper()}", ProviderName, "OAuth order capture complete.");
    }
}
