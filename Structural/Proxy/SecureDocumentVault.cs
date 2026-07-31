using System;
using System.Collections.Generic;

namespace DesignPatterns.Structural.Proxy;

// ============================================================
// Domain Models
// ============================================================

public record Document(string DocumentId, string Title, string Classification, string Content);

public record UserContext(string UserId, string Name, string Role);  // Role: "Admin", "Manager", "Employee", "Guest"

public record AccessLogEntry(DateTime Timestamp, string UserId, string UserRole, string DocumentId, string Action, bool WasGranted);

// ============================================================
// Subject Interface
// ============================================================

public interface IDocumentVault
{
    Document? GetDocument(string documentId, UserContext user);
    List<string> ListDocuments(UserContext user);
}

// ============================================================
// Real Subject — Actual document storage
// ============================================================

public class ConfidentialDocumentVault : IDocumentVault
{
    private readonly Dictionary<string, Document> _documents = new()
    {
        ["DOC-001"] = new Document("DOC-001", "Q4 Financial Report", "Confidential",
            "Revenue: $12.4M | Net Profit: $3.1M | Projected Growth: 18%"),
        ["DOC-002"] = new Document("DOC-002", "Employee Salary Matrix", "Restricted",
            "Engineering Avg: $145K | Marketing Avg: $110K | Executive Avg: $280K"),
        ["DOC-003"] = new Document("DOC-003", "Product Roadmap 2026", "Internal",
            "Phase 1: AI Integration | Phase 2: Global Expansion | Phase 3: IPO Prep"),
        ["DOC-004"] = new Document("DOC-004", "Company Holiday Calendar", "Public",
            "Jan 1, Mar 14, Jul 4, Nov 28, Dec 25"),
    };

    public Document? GetDocument(string documentId, UserContext user)
    {
        if (_documents.TryGetValue(documentId, out var doc))
        {
            Console.WriteLine($"  [Vault] Retrieved document: \"{doc.Title}\" [{doc.Classification}]");
            return doc;
        }
        Console.WriteLine($"  [Vault] Document {documentId} not found.");
        return null;
    }

    public List<string> ListDocuments(UserContext user)
    {
        return new List<string>(_documents.Keys);
    }
}

// ============================================================
// THE PROXY — Protection + Logging Proxy
// ============================================================

/// <summary>
/// SecureDocumentProxy wraps the real ConfidentialDocumentVault and adds:
/// 1. Role-based access control (RBAC) — checks user role against document classification
/// 2. Audit logging — records every access attempt (granted or denied)
/// </summary>
public class SecureDocumentProxy : IDocumentVault
{
    private readonly IDocumentVault _realVault;
    private readonly List<AccessLogEntry> _auditLog = new();

    // Classification → minimum required role
    private static readonly Dictionary<string, List<string>> ClassificationAccess = new()
    {
        ["Public"] = new List<string> { "Guest", "Employee", "Manager", "Admin" },
        ["Internal"] = new List<string> { "Employee", "Manager", "Admin" },
        ["Confidential"] = new List<string> { "Manager", "Admin" },
        ["Restricted"] = new List<string> { "Admin" },
    };

    public SecureDocumentProxy(IDocumentVault realVault)
    {
        _realVault = realVault;
    }

    public Document? GetDocument(string documentId, UserContext user)
    {
        Console.WriteLine($"\n  🔐 [Proxy] Access request: User \"{user.Name}\" (Role: {user.Role}) → Document {documentId}");

        // First, retrieve the document metadata to check classification
        var doc = _realVault.GetDocument(documentId, user);
        if (doc == null)
        {
            LogAccess(user, documentId, "GET_DOCUMENT", false);
            return null;
        }

        // Check access
        if (!IsAuthorized(user.Role, doc.Classification))
        {
            Console.WriteLine($"  🚫 [Proxy] ACCESS DENIED — {user.Role} cannot access {doc.Classification} documents.");
            LogAccess(user, documentId, "GET_DOCUMENT", false);
            return null;
        }

        Console.WriteLine($"  ✅ [Proxy] ACCESS GRANTED — Returning \"{doc.Title}\" to {user.Name}.");
        LogAccess(user, documentId, "GET_DOCUMENT", true);
        return doc;
    }

    public List<string> ListDocuments(UserContext user)
    {
        Console.WriteLine($"\n  🔐 [Proxy] List request from \"{user.Name}\" (Role: {user.Role})");
        LogAccess(user, "ALL", "LIST_DOCUMENTS", true);
        return _realVault.ListDocuments(user);
    }

    private bool IsAuthorized(string userRole, string classification)
    {
        if (ClassificationAccess.TryGetValue(classification, out var allowedRoles))
        {
            return allowedRoles.Contains(userRole);
        }
        return false;
    }

    private void LogAccess(UserContext user, string documentId, string action, bool wasGranted)
    {
        _auditLog.Add(new AccessLogEntry(DateTime.Now, user.UserId, user.Role, documentId, action, wasGranted));
    }

    /// <summary>
    /// Prints the complete audit trail — a key benefit of the Proxy pattern.
    /// </summary>
    public void PrintAuditLog()
    {
        Console.WriteLine("\n  📋 AUDIT LOG:");
        Console.WriteLine("  ───────────────────────────────────────────────────────────────────────");
        Console.WriteLine($"  {"Timestamp",-22} | {"User",-8} | {"Role",-10} | {"Document",-10} | {"Action",-16} | {"Granted"}");
        Console.WriteLine("  ───────────────────────────────────────────────────────────────────────");
        foreach (var entry in _auditLog)
        {
            Console.WriteLine($"  {entry.Timestamp:HH:mm:ss.fff}              | {entry.UserId,-8} | {entry.UserRole,-10} | {entry.DocumentId,-10} | {entry.Action,-16} | {(entry.WasGranted ? "✅ Yes" : "🚫 No")}");
        }
    }
}
