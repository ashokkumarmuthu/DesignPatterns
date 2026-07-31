using System;

namespace DesignPatterns.Structural.Proxy;

public class Program
{
    public static void RunDemo()
    {
        Console.WriteLine("=== PROXY PATTERN DEMO ===");
        Console.WriteLine("Problem Solved: Access control & audit logging without modifying the real vault\n");

        // The client works with IDocumentVault — doesn't know it's a proxy!
        var proxy = new SecureDocumentProxy(new ConfidentialDocumentVault());
        IDocumentVault vault = proxy;

        // Define users with different roles
        var admin = new UserContext("U001", "Ashok", "Admin");
        var manager = new UserContext("U002", "Priya", "Manager");
        var employee = new UserContext("U003", "Raj", "Employee");
        var guest = new UserContext("U004", "Visitor", "Guest");

        // Admin accessing Restricted document — should succeed
        var doc1 = vault.GetDocument("DOC-002", admin);
        if (doc1 != null) Console.WriteLine($"  Content: {doc1.Content}");

        // Manager accessing Confidential document — should succeed
        var doc2 = vault.GetDocument("DOC-001", manager);
        if (doc2 != null) Console.WriteLine($"  Content: {doc2.Content}");

        // Employee accessing Confidential document — should be DENIED
        var doc3 = vault.GetDocument("DOC-001", employee);

        // Guest accessing Internal document — should be DENIED
        var doc4 = vault.GetDocument("DOC-003", guest);

        // Guest accessing Public document — should succeed
        var doc5 = vault.GetDocument("DOC-004", guest);
        if (doc5 != null) Console.WriteLine($"  Content: {doc5.Content}");

        // Print the full audit trail
        proxy.PrintAuditLog();

        Console.WriteLine("\n==========================\n");
    }
}
