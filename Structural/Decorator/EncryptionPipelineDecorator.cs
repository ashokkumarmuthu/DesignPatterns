using System;
using System.Text;

namespace DesignPatterns.Structural.Decorator;

public class EncryptionPipelineDecorator(IApiResponsePipeline inner, string secretKey) : PipelineDecorator(inner)
{
    private readonly string _secretKey = secretKey;

    public override string ProcessResponse(string rawJsonPayload)
    {
        string input = base.ProcessResponse(rawJsonPayload);
        string encrypted = $"[AES256_ENCRYPTED(Key={_secretKey[..4]}***): {Convert.ToBase64String(Encoding.UTF8.GetBytes(input))[..Math.Min(30, input.Length)]}...]";
        Console.WriteLine($"  🔒 [Encryption Layer] Encrypted payload using key '{_secretKey[..4]}***'");
        return encrypted;
    }
}
