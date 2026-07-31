using System;

namespace DesignPatterns.Behavioral.ChainOfResponsibility;

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== CHAIN OF RESPONSIBILITY PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Expense claims auto-route through approval hierarchy\n");

        // Build the chain: Team Lead → Manager → Director → CFO
        var teamLead = new TeamLeadApprover("Ravi Singh");
        var manager = new ManagerApprover("Meera Patel");
        var director = new DirectorApprover("Vikram Rao");
        var cfo = new CfoApprover("Sunita Nair");

        teamLead.SetNext(manager).SetNext(director).SetNext(cfo);

        // Submit expenses of varying amounts — watch them route through the chain
        var expenses = new[]
        {
            new ExpenseClaim("Ashok Kumar", "Engineering", 250.00m, "Team lunch", "Client Entertainment"),
            new ExpenseClaim("Priya Sharma", "Marketing", 3_500.00m, "Conference sponsorship booth", "Training"),
            new ExpenseClaim("Raj Patel", "Sales", 18_000.00m, "Annual client retreat venue booking", "Client Entertainment"),
            new ExpenseClaim("Deepa Menon", "Operations", 75_000.00m, "New warehouse equipment procurement", "Equipment"),
        };

        foreach (var expense in expenses)
        {
            Console.WriteLine($"\n  📋 Expense Claim: {expense.EmployeeName} ({expense.Department}) — ${expense.Amount:F2} \"{expense.Description}\"");
            Console.WriteLine("  ────────────────────────────────────────────────");
            var result = teamLead.Handle(expense);
            Console.WriteLine($"  📌 Final Result: {result.Status} by {result.ApproverTitle} — {result.Comments}");
        }

        Console.WriteLine("\n==============================================\n");
    }
}
