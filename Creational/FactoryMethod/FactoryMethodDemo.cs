using System;

namespace DesignPatterns.Creational.FactoryMethod;

// Problem Solved: Encapsulating notification channel creation logic
public interface INotification
{
    void Send(string message);
}

public class EmailNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"  [Email] Sending: \"{message}\" via SMTP server.");
}

public class SmsNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"  [SMS] Sending: \"{message}\" via Twilio API.");
}

public class PushNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"  [Push] Sending: \"{message}\" via Firebase.");
}

public abstract class NotificationCreator
{
    public abstract INotification CreateNotification();

    public void Notify(string message)
    {
        INotification notification = CreateNotification();
        notification.Send(message);
    }
}

public class EmailNotificationCreator : NotificationCreator
{
    public override INotification CreateNotification() => new EmailNotification();
}

public class SmsNotificationCreator : NotificationCreator
{
    public override INotification CreateNotification() => new SmsNotification();
}

public class PushNotificationCreator : NotificationCreator
{
    public override INotification CreateNotification() => new PushNotification();
}

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== FACTORY METHOD PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Decoupling client from concrete notification channel creation\n");

        NotificationCreator emailCreator = new EmailNotificationCreator();
        emailCreator.Notify("Your order has been shipped!");

        NotificationCreator smsCreator = new SmsNotificationCreator();
        smsCreator.Notify("Your OTP is 482910.");

        Console.WriteLine("===================================\n");
    }
}
