using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesignPatterns;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("        DESIGN PATTERNS COMPREHENSIVE LEARNING & CHALLENGE HUB            ");
            Console.WriteLine("==========================================================================");
            Console.WriteLine();
            Console.WriteLine("--- QUICK DEMOS vs FULL MULTI-FILE PROJECTS ---");
            Console.WriteLine(" 1) Strategy Pattern              -> Demo [1D] | Full Project [1F]");
            Console.WriteLine(" 2) Observer Pattern              -> Demo [2D] | Full Project [2F]");
            Console.WriteLine(" 3) Command Pattern               -> Demo [3D] | Full Project [3F]");
            Console.WriteLine(" 4) Factory Method Pattern        -> Demo [4D] | Full Project [4F]");
            Console.WriteLine(" 5) Builder Pattern               -> Demo [5D] | Full Project [5F]");
            Console.WriteLine(" 6) Singleton Pattern             -> Demo [6D] | Full Project [6F]");
            Console.WriteLine(" 7) Decorator Pattern             -> Demo [7D] | Full Project [7F]");
            Console.WriteLine(" 8) Adapter Pattern               -> Demo [8D] | Full Project [8F]");
            Console.WriteLine(" 9) Facade Pattern                -> Demo [9D] | Full Project [9F]");
            Console.WriteLine("10) Template Method Pattern       -> Demo [10D] | Full Project [10F]");
            Console.WriteLine("11) State Pattern                 -> Demo [11D] | Full Project [11F]");
            Console.WriteLine("12) Chain of Responsibility       -> Demo [12D] | Full Project [12F]");
            Console.WriteLine("13) Proxy Pattern                 -> Demo [13D] | Full Project [13F]");
            Console.WriteLine();
            Console.WriteLine("ALL_DEMOS) Run all 13 Quick Demos");
            Console.WriteLine("ALL_FULL)  Run all 13 Full Real-World Projects");
            Console.WriteLine("Q)         Quit");
            Console.Write("\nEnter choice (e.g. 1F for Strategy Full Project): ");

            var input = Console.ReadLine()?.Trim().ToUpper();

            if (input == "Q")
            {
                Console.WriteLine("Exiting runner...");
                break;
            }

            Console.WriteLine();

            switch (input)
            {
                case "1D": Behavioral.Strategy.Program.RunDemo(); break;
                case "1F": RunStrategyProject(); break;

                case "2D": Behavioral.Observer.Program.RunDemo(); break;
                case "2F": RunObserverProject(); break;

                case "3D": Behavioral.Command.Program.RunDemo(); break;
                case "3F": RunCommandProject(); break;

                case "4D": Creational.FactoryMethod.Program.RunDemo(); break;
                case "4F": RunFactoryMethodProject(); break;

                case "5D": Creational.Builder.Program.RunDemo(); break;
                case "5F": RunBuilderProject(); break;

                case "6D": Creational.Singleton.Program.RunDemo(); break;
                case "6F": RunSingletonProject(); break;

                case "7D": Structural.Decorator.Program.RunDemo(); break;
                case "7F": RunDecoratorProject(); break;

                case "8D": Structural.Adapter.Program.RunDemo(); break;
                case "8F": RunAdapterProject(); break;

                case "9D": Structural.Facade.Program.RunDemo(); break;
                case "9F": RunFacadeProject(); break;

                case "10D": Behavioral.TemplateMethod.Program.RunDemo(); break;
                case "10F": RunTemplateMethodProject(); break;

                case "11D": Behavioral.State.Program.RunDemo(); break;
                case "11F": RunStateProject(); break;

                case "12D": Behavioral.ChainOfResponsibility.Program.RunDemo(); break;
                case "12F": RunChainOfResponsibilityProject(); break;

                case "13D": Structural.Proxy.Program.RunDemo(); break;
                case "13F": RunProxyProject(); break;

                case "ALL_DEMOS": RunAllDemos(); break;
                case "ALL_FULL": RunAllFullProjects(); break;

                default:
                    Console.WriteLine("Invalid selection. Try entering '1F', '9D', or 'ALL_FULL'.\n");
                    break;
            }
        }
    }

    private static void RunStrategyProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Enterprise E-Commerce Logistics & Shipping Strategy Engine");
        Console.WriteLine("==========================================================================");

        var engine = new Behavioral.Strategy.LogisticsEngine();
        engine.RegisterCarrier(new Behavioral.Strategy.FedExStandardStrategy());
        engine.RegisterCarrier(new Behavioral.Strategy.DhlExpressStrategy());
        engine.RegisterCarrier(new Behavioral.Strategy.AmazonSameDayStrategy());

        var domesticOrder = new Behavioral.Strategy.ShipmentOrder("ORD-99201", 4.2, 350, "USA", 65.00m, true);
        Console.WriteLine($"\n📦 Order #{domesticOrder.OrderId} (Weight: {domesticOrder.WeightKg}kg, Dest: {domesticOrder.DestinationCountry}, Prime: {domesticOrder.IsPrimeMember})");
        Console.WriteLine("--------------------------------------------------------------------------");
        
        foreach (var q in engine.CompareAllQuotes(domesticOrder))
        {
            Console.WriteLine($"• {q.CarrierName,-30} | Cost: {q.TotalCost,7:C} | Est: {q.EstimatedDays} day(s) | {q.BreakdownDetails}");
        }

        var cheapest = engine.FindCheapestQuote(domesticOrder);
        Console.WriteLine($"\n✅ Recommended Best Value Carrier: {cheapest.CarrierName} at {cheapest.TotalCost:C}");
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunObserverProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Real-Time Stock Market Ticker & Multi-Observer Alert System");
        Console.WriteLine("==========================================================================");

        var ticker = new Behavioral.Observer.MarketTicker();
        ticker.RegisterStock("NVDA", 120.00m);

        var traderBot = new Behavioral.Observer.AutoTraderBot("BOT-01", "NVDA", buyThreshold: 112.00m, sellThreshold: 135.00m);
        var investor = new Behavioral.Observer.InvestorNotifier("Ashok Kumar", "ashok@example.com");
        var auditLogger = new Behavioral.Observer.FinancialAuditLogger();

        ticker.Subscribe(traderBot);
        ticker.Subscribe(investor);
        ticker.Subscribe(auditLogger);

        ticker.UpdatePrice("NVDA", 110.00m);
        ticker.UpdatePrice("NVDA", 138.00m);
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunCommandProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Smart Home Automation Remote & Macro Execution Pipeline");
        Console.WriteLine("==========================================================================");

        var livingRoomLight = new Behavioral.Command.SmartLight("Living Room");
        var kitchenLight = new Behavioral.Command.SmartLight("Kitchen");
        var thermostat = new Behavioral.Command.SmartThermostat("Main Hall");
        var frontDoorLock = new Behavioral.Command.SmartLock("Front Door");

        var remote = new Behavioral.Command.SmartHomeRemoteController();

        var goodNightRoutine = new Behavioral.Command.MacroCommand("Good Night Routine", new List<Behavioral.Command.ISmartHomeCommand>
        {
            new Behavioral.Command.LightCommand(livingRoomLight, turnOn: false),
            new Behavioral.Command.LightCommand(kitchenLight, turnOn: false),
            new Behavioral.Command.ThermostatCommand(thermostat, targetTemp: 65),
            new Behavioral.Command.LockCommand(frontDoorLock, shouldLock: true)
        });

        remote.PressButton(goodNightRoutine);
        remote.PressUndoButton();
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunFactoryMethodProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Multi-Tenant Payment Processor Dispatcher (Factory Method)");
        Console.WriteLine("==========================================================================");

        var usTx = new Creational.FactoryMethod.PaymentRequest("TXN-1001", 149.99m, "USD", "MERCHANT-US");
        Creational.FactoryMethod.PaymentProcessorFactory factory = new Creational.FactoryMethod.StripeProcessorFactory("sk_live_992103912093");
        var res1 = factory.Process(usTx);
        Console.WriteLine($"  Result: Status={(res1.IsSuccess ? "Success ✅" : "Failed ❌")}, Ref={res1.TransactionReference}, Msg='{res1.Message}'");
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunBuilderProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Fluent SQL & Analytical Query Builder");
        Console.WriteLine("==========================================================================");

        var query = new Creational.Builder.SqlQueryBuilder()
            .From("Users u")
            .Select("u.Id", "u.Name", "COUNT(o.Id) AS TotalOrders")
            .InnerJoin("Orders o", "u.Id = o.UserId")
            .Where("u.IsActive = 1")
            .GroupBy("u.Id", "u.Name")
            .Having("COUNT(o.Id) > 5")
            .OrderBy("TotalOrders", descending: true)
            .Paginate(pageNumber: 1, pageSize: 10)
            .Build();

        Console.WriteLine(query.ToExecutableSql());
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunSingletonProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Enterprise Thread-Safe App Configuration & Cache Manager");
        Console.WriteLine("==========================================================================");

        var config = Creational.Singleton.AppConfigurationManager.Instance;
        config.PrintAllSettings();
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunDecoratorProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Secure HTTP API Response Middleware Pipeline (Decorator)");
        Console.WriteLine("==========================================================================");

        Structural.Decorator.IApiResponsePipeline pipeline = new Structural.Decorator.TelemetryPipelineDecorator(
            new Structural.Decorator.EncryptionPipelineDecorator(
                new Structural.Decorator.CompressionPipelineDecorator(
                    new Structural.Decorator.RawJsonPipeline()
                ), secretKey: "SecretKey_991823"
            ), endpoint: "/api/v1/checkout"
        );

        string output = pipeline.ProcessResponse("{\"orderId\": \"ORD-9910\", \"user\": \"ashok\", \"amount\": 450.00}");
        Console.WriteLine($"\nOutput:\n{output}");
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunAdapterProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Enterprise Freight Logistics & Legacy SOAP SDK Adapter");
        Console.WriteLine("==========================================================================");

        var request = new Structural.Adapter.FreightShipmentRequest("FRG-88192", "Port of Shanghai", "Port of Los Angeles", 1250.0);
        var legacyClient = new Structural.Adapter.LegacySoapFreightClient();
        Structural.Adapter.IFreightProvider adaptedProvider = new Structural.Adapter.LegacyMaritimeLogisticsAdapter(legacyClient);
        var status = adaptedProvider.DispatchFreight(request);
        Console.WriteLine($"Result: {adaptedProvider.ProviderName} -> Status: {status.Status}, Est: {status.EstimatedHours}h");
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunFacadeProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Hotel Room Booking System (Facade)");
        Console.WriteLine("==========================================================================");

        var facade = new Structural.Facade.HotelBookingFacade(
            new Structural.Facade.RoomInventoryService(),
            new Structural.Facade.PaymentService(),
            new Structural.Facade.GuestRegistryService(),
            new Structural.Facade.EmailNotificationService()
        );

        var request1 = new Structural.Facade.BookingRequest("Ashok Kumar", "ashok@example.com", "Suite",
            DateTime.Today.AddDays(14), DateTime.Today.AddDays(17), 4532015112830366m);

        var confirmation1 = facade.BookRoom(request1);
        Console.WriteLine($"\n📌 Result: {confirmation1.Status} | Room: {confirmation1.RoomNumber} | Total: {confirmation1.TotalCharged:C}");

        var request2 = new Structural.Facade.BookingRequest("Priya Sharma", "priya@example.com", "Standard",
            DateTime.Today.AddDays(3), DateTime.Today.AddDays(5), 5425233430109903m);

        var confirmation2 = facade.BookRoom(request2);
        Console.WriteLine($"\n📌 Result: {confirmation2.Status} | Room: {confirmation2.RoomNumber} | Total: {confirmation2.TotalCharged:C}");
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunTemplateMethodProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Data Export Pipeline — CSV, JSON, PDF (Template Method)");
        Console.WriteLine("==========================================================================");

        Console.WriteLine("\n--- Export 1: CSV Format ---");
        new Behavioral.TemplateMethod.CsvDataExporter().Export();

        Console.WriteLine("--- Export 2: JSON Format ---");
        new Behavioral.TemplateMethod.JsonDataExporter().Export();

        Console.WriteLine("--- Export 3: PDF Report Format ---");
        new Behavioral.TemplateMethod.PdfReportExporter().Export();

        Console.WriteLine("==========================================================================\n");
    }

    private static void RunStateProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: E-Commerce Order Fulfillment Lifecycle (State)");
        Console.WriteLine("==========================================================================");

        Console.WriteLine("\n--- Happy Path: Full Lifecycle ---");
        var order = new Behavioral.State.Order("ORD-7001", "Ashok Kumar", 599.99m);
        order.PrintStatus();
        order.Confirm();
        order.Ship();
        order.Deliver();
        order.PrintStatus();

        Console.WriteLine("\n--- Edge Case: Cancel After Confirmation ---");
        var order2 = new Behavioral.State.Order("ORD-7002", "Priya Sharma", 129.50m);
        order2.Confirm();
        order2.Cancel();
        order2.PrintStatus();

        Console.WriteLine("\n--- Edge Case: Invalid Skip Attempt ---");
        var order3 = new Behavioral.State.Order("ORD-7003", "Raj Patel", 850.00m);
        order3.Ship();     // Can't ship without confirming first
        order3.Deliver();  // Can't deliver without shipping first
        Console.WriteLine("==========================================================================\n");
    }

    private static void RunChainOfResponsibilityProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Corporate Expense Approval Pipeline (Chain of Responsibility)");
        Console.WriteLine("==========================================================================");

        var teamLead = new Behavioral.ChainOfResponsibility.TeamLeadApprover("Ravi Singh");
        var manager = new Behavioral.ChainOfResponsibility.ManagerApprover("Meera Patel");
        var director = new Behavioral.ChainOfResponsibility.DirectorApprover("Vikram Rao");
        var cfo = new Behavioral.ChainOfResponsibility.CfoApprover("Sunita Nair");

        teamLead.SetNext(manager).SetNext(director).SetNext(cfo);

        var expenses = new[]
        {
            new Behavioral.ChainOfResponsibility.ExpenseClaim("Ashok", "Engineering", 150.00m, "Office supplies", "Equipment"),
            new Behavioral.ChainOfResponsibility.ExpenseClaim("Priya", "Marketing", 2_800.00m, "Trade show booth rental", "Training"),
            new Behavioral.ChainOfResponsibility.ExpenseClaim("Raj", "Sales", 15_000.00m, "Client annual dinner event", "Client Entertainment"),
            new Behavioral.ChainOfResponsibility.ExpenseClaim("Deepa", "Operations", 50_000.00m, "Server infrastructure upgrade", "Equipment"),
        };

        foreach (var expense in expenses)
        {
            Console.WriteLine($"\n  📋 {expense.EmployeeName} ({expense.Department}): ${expense.Amount:F2} — \"{expense.Description}\"");
            Console.WriteLine("  ────────────────────────────────────────────────");
            var result = teamLead.Handle(expense);
            Console.WriteLine($"  📌 Result: {result.Status} by {result.ApproverTitle}");
        }
        Console.WriteLine("\n==========================================================================\n");
    }

    private static void RunProxyProject()
    {
        Console.WriteLine("==========================================================================");
        Console.WriteLine("  PROJECT: Secure Document Vault with Access Control & Audit (Proxy)");
        Console.WriteLine("==========================================================================");

        var proxy = new Structural.Proxy.SecureDocumentProxy(new Structural.Proxy.ConfidentialDocumentVault());

        var admin = new Structural.Proxy.UserContext("U001", "Ashok", "Admin");
        var manager = new Structural.Proxy.UserContext("U002", "Priya", "Manager");
        var employee = new Structural.Proxy.UserContext("U003", "Raj", "Employee");
        var guest = new Structural.Proxy.UserContext("U004", "Visitor", "Guest");

        Console.WriteLine("\n--- Admin accessing Restricted doc ---");
        var doc1 = proxy.GetDocument("DOC-002", admin);
        if (doc1 != null) Console.WriteLine($"  📄 Content: {doc1.Content}");

        Console.WriteLine("\n--- Manager accessing Confidential doc ---");
        var doc2 = proxy.GetDocument("DOC-001", manager);
        if (doc2 != null) Console.WriteLine($"  📄 Content: {doc2.Content}");

        Console.WriteLine("\n--- Employee accessing Confidential doc (DENIED) ---");
        proxy.GetDocument("DOC-001", employee);

        Console.WriteLine("\n--- Guest accessing Public doc ---");
        var doc4 = proxy.GetDocument("DOC-004", guest);
        if (doc4 != null) Console.WriteLine($"  📄 Content: {doc4.Content}");

        proxy.PrintAuditLog();
        Console.WriteLine("\n==========================================================================\n");
    }

    private static void RunAllDemos()
    {
        Behavioral.Strategy.Program.RunDemo();
        Behavioral.Observer.Program.RunDemo();
        Behavioral.Command.Program.RunDemo();
        Creational.FactoryMethod.Program.RunDemo();
        Creational.Builder.Program.RunDemo();
        Creational.Singleton.Program.RunDemo();
        Structural.Decorator.Program.RunDemo();
        Structural.Adapter.Program.RunDemo();
        Structural.Facade.Program.RunDemo();
        Behavioral.TemplateMethod.Program.RunDemo();
        Behavioral.State.Program.RunDemo();
        Behavioral.ChainOfResponsibility.Program.RunDemo();
        Structural.Proxy.Program.RunDemo();
    }

    private static void RunAllFullProjects()
    {
        RunStrategyProject();
        RunObserverProject();
        RunCommandProject();
        RunFactoryMethodProject();
        RunBuilderProject();
        RunSingletonProject();
        RunDecoratorProject();
        RunAdapterProject();
        RunFacadeProject();
        RunTemplateMethodProject();
        RunStateProject();
        RunChainOfResponsibilityProject();
        RunProxyProject();
    }
}
