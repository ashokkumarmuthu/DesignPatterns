using System;

namespace DesignPatterns.Structural.Facade;

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== FACADE PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Simplifying complex multi-subsystem hotel booking into one call\n");

        // Without Facade, the client would need to manually:
        //   1. Call RoomInventoryService.CheckAvailability()
        //   2. Calculate pricing
        //   3. Call PaymentService.ChargeCard()
        //   4. Call RoomInventoryService.ReserveRoom()
        //   5. Call GuestRegistryService.RegisterGuest()
        //   6. Call EmailNotificationService.SendConfirmationEmail()
        //   ... and handle rollback if any step fails!

        // With Facade — ONE call does everything:
        var facade = new HotelBookingFacade(
            new RoomInventoryService(),
            new PaymentService(),
            new GuestRegistryService(),
            new EmailNotificationService()
        );

        var request = new BookingRequest(
            GuestName: "Ashok Kumar",
            Email: "ashok@example.com",
            RoomType: "Deluxe",
            CheckIn: DateTime.Today.AddDays(7),
            CheckOut: DateTime.Today.AddDays(10),
            CreditCardNumber: 4532015112830366m
        );

        var confirmation = facade.BookRoom(request);

        Console.WriteLine($"\n📌 Booking Result: {confirmation.Status}");
        Console.WriteLine($"   Confirmation: {confirmation.ConfirmationId} | Room: {confirmation.RoomNumber} | Charged: {confirmation.TotalCharged:C}");
        Console.WriteLine("===========================\n");
    }
}
