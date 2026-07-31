using System;
using System.Collections.Generic;

namespace DesignPatterns.Creational.Builder;

// Problem Solved: Fluent construction of complex objects with optional parameters
public class Email
{
    public string To { get; init; } = string.Empty;
    public string From { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public string? Cc { get; init; }
    public string? Bcc { get; init; }
    public bool IsHtml { get; init; }
    public List<string> Attachments { get; init; } = new();

    public void Display()
    {
        Console.WriteLine($"  From:    {From}");
        Console.WriteLine($"  To:      {To}");
        if (Cc != null) Console.WriteLine($"  CC:      {Cc}");
        Console.WriteLine($"  Subject: {Subject}");
        Console.WriteLine($"  Body:    {Body}");
        Console.WriteLine($"  HTML:    {IsHtml}");
        if (Attachments.Count > 0)
            Console.WriteLine($"  Files:   {string.Join(", ", Attachments)}");
    }
}

public class EmailBuilder
{
    private string _to = string.Empty;
    private string _from = string.Empty;
    private string _subject = string.Empty;
    private string _body = string.Empty;
    private string? _cc;
    private string? _bcc;
    private bool _isHtml;
    private readonly List<string> _attachments = new();

    public EmailBuilder To(string to) { _to = to; return this; }
    public EmailBuilder From(string from) { _from = from; return this; }
    public EmailBuilder Subject(string subject) { _subject = subject; return this; }
    public EmailBuilder Body(string body) { _body = body; return this; }
    public EmailBuilder Cc(string cc) { _cc = cc; return this; }
    public EmailBuilder Bcc(string bcc) { _bcc = bcc; return this; }
    public EmailBuilder AsHtml() { _isHtml = true; return this; }
    public EmailBuilder Attach(string filename) { _attachments.Add(filename); return this; }

    public Email Build()
    {
        if (string.IsNullOrEmpty(_to) || string.IsNullOrEmpty(_from))
            throw new InvalidOperationException("Email must have a recipient and sender.");

        return new Email
        {
            To = _to,
            From = _from,
            Subject = _subject,
            Body = _body,
            Cc = _cc,
            Bcc = _bcc,
            IsHtml = _isHtml,
            Attachments = new List<string>(_attachments)
        };
    }
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== BUILDER PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Eliminating telescoping constructors for complex Email objects\n");

        var email = new EmailBuilder()
            .From("billing@company.com")
            .To("user@example.com")
            .Cc("manager@company.com")
            .Subject("Invoice #1042")
            .Body("<h1>Your invoice is attached.</h1>")
            .AsHtml()
            .Attach("invoice_1042.pdf")
            .Build();

        email.Display();
        Console.WriteLine("============================\n");
    }
}
