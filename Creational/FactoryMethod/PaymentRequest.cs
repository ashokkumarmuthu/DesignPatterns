namespace DesignPatterns.Creational.FactoryMethod;

public record PaymentRequest(
    string TransactionId,
    decimal Amount,
    string Currency,
    string MerchantId
);
