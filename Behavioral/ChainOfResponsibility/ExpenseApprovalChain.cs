using System;

namespace DesignPatterns.Behavioral.ChainOfResponsibility;

// ============================================================
// Domain Models
// ============================================================

public record ExpenseClaim(
    string EmployeeName,
    string Department,
    decimal Amount,
    string Description,
    string Category  // "Travel", "Equipment", "Training", "Client Entertainment"
);

public record ApprovalResult(
    string ApprovedBy,
    string ApproverTitle,
    decimal Amount,
    string Status,    // "APPROVED", "ESCALATED", "REJECTED"
    string Comments
);

// ============================================================
// Abstract Handler — Base class for the chain
// ============================================================

/// <summary>
/// Each approver has a spending limit. If the expense is within their limit,
/// they approve it. Otherwise, they escalate to the next approver in the chain.
/// </summary>
public abstract class ExpenseApprover
{
    private ExpenseApprover? _nextApprover;

    public string Name { get; }
    public string Title { get; }
    protected decimal ApprovalLimit { get; }

    protected ExpenseApprover(string name, string title, decimal approvalLimit)
    {
        Name = name;
        Title = title;
        ApprovalLimit = approvalLimit;
    }

    /// <summary>
    /// Chains the next approver. Returns the next approver for fluent chaining.
    /// </summary>
    public ExpenseApprover SetNext(ExpenseApprover nextApprover)
    {
        _nextApprover = nextApprover;
        return nextApprover;
    }

    /// <summary>
    /// Processes the expense claim. Approves if within limit, escalates otherwise.
    /// </summary>
    public virtual ApprovalResult Handle(ExpenseClaim claim)
    {
        if (claim.Amount <= ApprovalLimit)
        {
            Console.WriteLine($"  ✅ {Title} ({Name}) APPROVED ${claim.Amount:F2} — \"{claim.Description}\"");
            return new ApprovalResult(Name, Title, claim.Amount, "APPROVED",
                $"Within {Title} approval limit of ${ApprovalLimit:F2}");
        }

        if (_nextApprover != null)
        {
            Console.WriteLine($"  ⬆️  {Title} ({Name}) cannot approve ${claim.Amount:F2} (limit: ${ApprovalLimit:F2}). Escalating to {_nextApprover.Title}...");
            return _nextApprover.Handle(claim);
        }

        Console.WriteLine($"  ❌ ${claim.Amount:F2} exceeds all approval limits. Expense REJECTED.");
        return new ApprovalResult("N/A", "N/A", claim.Amount, "REJECTED",
            "Exceeds maximum approval authority in the chain.");
    }
}

// ============================================================
// Concrete Handlers
// ============================================================

public class TeamLeadApprover : ExpenseApprover
{
    public TeamLeadApprover(string name) : base(name, "Team Lead", 500.00m) { }
}

public class ManagerApprover : ExpenseApprover
{
    public ManagerApprover(string name) : base(name, "Manager", 5_000.00m) { }
}

public class DirectorApprover : ExpenseApprover
{
    public DirectorApprover(string name) : base(name, "Director", 25_000.00m) { }
}

/// <summary>
/// CFO is the final approver — can approve any amount.
/// </summary>
public class CfoApprover : ExpenseApprover
{
    public CfoApprover(string name) : base(name, "CFO", decimal.MaxValue) { }
}
