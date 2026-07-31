using System;

namespace DesignPatterns.Creational.FactoryMethod;

public class RazorpayPaymentProcessor(string keyId) : IPaymentProcessor
{
    public string ProviderName => "Razorpay UPI & NetBanking";
    private readonly string _keyId = keyId;

    public PaymentResult ExecutePayment(PaymentRequest request)
    {
        Console.WriteLine($"  [Razorpay SDK] Verifying Key ID '{_keyId}' for INR transaction...");
        Console.WriteLine($"  [Razorpay SDK] Dispatched UPI Payment Request of ₹{request.Amount:F2}...");
        return new PaymentResult(true, $"pay_rzp_{Guid.NewGuid().ToString()[..8]}", ProviderName, "UPI Payment authorized via VPA.");
    }
}
