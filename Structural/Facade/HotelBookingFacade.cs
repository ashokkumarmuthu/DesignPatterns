using System;

namespace DesignPatterns.Structural.Facade;

// ============================================================
// Domain Models
// ============================================================

public record BookingRequest(
    string GuestName,
    string Email,
    string RoomType,         // "Standard", "Deluxe", "Suite"
    DateTime CheckIn,
    DateTime CheckOut,
    decimal CreditCardNumber  // Simplified for demo
);

public record BookingConfirmation(
    string ConfirmationId,
    string GuestName,
    string RoomNumber,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalCharged,
    string Status
);

// ============================================================
// Subsystem 1: Room Inventory
// ============================================================

public interface IRoomInventory
{
    bool CheckAvailability(string roomType, DateTime checkIn, DateTime checkOut);
    string ReserveRoom(string roomType, DateTime checkIn, DateTime checkOut);
    void ReleaseRoom(string roomNumber);
}

public class RoomInventoryService : IRoomInventory
{
    public bool CheckAvailability(string roomType, DateTime checkIn, DateTime checkOut)
    {
        Console.WriteLine($"  [RoomInventory] Checking availability for {roomType} ({checkIn:MMM dd} - {checkOut:MMM dd})...");
        return true; // Simplified: always available
    }

    public string ReserveRoom(string roomType, DateTime checkIn, DateTime checkOut)
    {
        string roomNumber = roomType switch
        {
            "Suite" => "S-401",
            "Deluxe" => "D-215",
            _ => "R-108"
        };
        Console.WriteLine($"  [RoomInventory] Reserved room {roomNumber} ({roomType}).");
        return roomNumber;
    }

    public void ReleaseRoom(string roomNumber)
    {
        Console.WriteLine($"  [RoomInventory] Released room {roomNumber} back to available pool.");
    }
}

// ============================================================
// Subsystem 2: Payment Processing
// ============================================================

public interface IPaymentService
{
    bool ChargeCard(decimal cardNumber, decimal amount, string description);
    void RefundCard(decimal cardNumber, decimal amount);
}

public class PaymentService : IPaymentService
{
    public bool ChargeCard(decimal cardNumber, decimal amount, string description)
    {
        Console.WriteLine($"  [Payment] Charged ${amount:F2} to card ending ****{cardNumber % 10000:0000} — {description}");
        return true;
    }

    public void RefundCard(decimal cardNumber, decimal amount)
    {
        Console.WriteLine($"  [Payment] Refunded ${amount:F2} to card ending ****{cardNumber % 10000:0000}");
    }
}

// ============================================================
// Subsystem 3: Guest Registration
// ============================================================

public interface IGuestRegistry
{
    string RegisterGuest(string guestName, string email, string roomNumber);
}

public class GuestRegistryService : IGuestRegistry
{
    public string RegisterGuest(string guestName, string email, string roomNumber)
    {
        string guestId = $"G-{Math.Abs(guestName.GetHashCode()) % 10000:D4}";
        Console.WriteLine($"  [GuestRegistry] Registered {guestName} (ID: {guestId}) in room {roomNumber}.");
        return guestId;
    }
}

// ============================================================
// Subsystem 4: Notification
// ============================================================

public interface INotificationService
{
    void SendConfirmationEmail(string email, string confirmationId, string roomNumber, DateTime checkIn, DateTime checkOut);
}

public class EmailNotificationService : INotificationService
{
    public void SendConfirmationEmail(string email, string confirmationId, string roomNumber, DateTime checkIn, DateTime checkOut)
    {
        Console.WriteLine($"  [Notification] Sent booking confirmation #{confirmationId} to {email}");
        Console.WriteLine($"                 Room: {roomNumber} | {checkIn:MMM dd, yyyy} → {checkOut:MMM dd, yyyy}");
    }
}

// ============================================================
// THE FACADE — Single entry point orchestrating all subsystems
// ============================================================

public class HotelBookingFacade
{
    private readonly IRoomInventory _roomInventory;
    private readonly IPaymentService _paymentService;
    private readonly IGuestRegistry _guestRegistry;
    private readonly INotificationService _notificationService;

    public HotelBookingFacade(
        IRoomInventory roomInventory,
        IPaymentService paymentService,
        IGuestRegistry guestRegistry,
        INotificationService notificationService)
    {
        _roomInventory = roomInventory;
        _paymentService = paymentService;
        _guestRegistry = guestRegistry;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Single simplified method that orchestrates 4 complex subsystems.
    /// The client doesn't need to know about room inventory, payment processing,
    /// guest registration, or email notifications individually.
    /// </summary>
    public BookingConfirmation BookRoom(BookingRequest request)
    {
        Console.WriteLine($"\n📋 Processing booking for {request.GuestName}...\n");

        // Step 1: Check room availability
        if (!_roomInventory.CheckAvailability(request.RoomType, request.CheckIn, request.CheckOut))
        {
            return new BookingConfirmation("N/A", request.GuestName, "N/A",
                request.CheckIn, request.CheckOut, 0, "FAILED — No Availability");
        }

        // Step 2: Calculate cost & charge payment
        int nights = (request.CheckOut - request.CheckIn).Days;
        decimal ratePerNight = request.RoomType switch
        {
            "Suite" => 350.00m,
            "Deluxe" => 200.00m,
            _ => 120.00m
        };
        decimal totalCost = nights * ratePerNight;

        if (!_paymentService.ChargeCard(request.CreditCardNumber, totalCost, $"{nights} nights x ${ratePerNight}/night"))
        {
            return new BookingConfirmation("N/A", request.GuestName, "N/A",
                request.CheckIn, request.CheckOut, 0, "FAILED — Payment Declined");
        }

        // Step 3: Reserve room
        string roomNumber = _roomInventory.ReserveRoom(request.RoomType, request.CheckIn, request.CheckOut);

        // Step 4: Register guest
        _guestRegistry.RegisterGuest(request.GuestName, request.Email, roomNumber);

        // Step 5: Send confirmation
        string confirmationId = $"BK-{DateTime.Now:yyyyMMdd}-{Math.Abs(request.GuestName.GetHashCode()) % 10000:D4}";
        _notificationService.SendConfirmationEmail(request.Email, confirmationId, roomNumber, request.CheckIn, request.CheckOut);

        return new BookingConfirmation(confirmationId, request.GuestName, roomNumber,
            request.CheckIn, request.CheckOut, totalCost, "CONFIRMED ✅");
    }
}
