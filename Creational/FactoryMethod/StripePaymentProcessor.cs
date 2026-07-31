using System;

namespace DesignPatterns.Creational.FactoryMethod;

public class StripePaymentProcessor(string apiKey) : IPaymentProcessor
{
    public string ProviderName => "Stripe Direct Gateway";
    private readonly string _apiKey = apiKey;

    public PaymentResult ExecutePayment(PaymentRequest request)
    {
        Console.WriteLine($"  [Stripe SDK] Authenticating with Secret Key '{_apiKey[..6]}***'...");
        Console.WriteLine($"  [Stripe SDK] Charging {request.Amount:C} ({request.Currency}) for Merchant {request.MerchantId}...");
        return new PaymentResult(true, $"ch_stripe_{Guid.NewGuid().ToString()[..8]}", ProviderName, "Charge successful via 3D Secure.");
    }
}
