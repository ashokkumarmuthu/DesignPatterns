namespace DesignPatterns.Creational.FactoryMethod;

public record PaymentResult(
    bool IsSuccess,
    string TransactionReference,
    string ProviderName,
    string Message
);
