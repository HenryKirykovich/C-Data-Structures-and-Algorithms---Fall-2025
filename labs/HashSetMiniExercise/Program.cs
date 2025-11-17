using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("HashSet Mini Exercise — Demo\n");

        // If an argument is provided, run that scenario; otherwise run all demos.
        if (args.Length > 0)
        {
            var arg = args[0].ToLowerInvariant();
            if (arg == "a") ShowScenarioA();
            else if (arg == "b") ShowScenarioB();
            else if (arg == "c") ShowScenarioC();
            else
            {
                Console.WriteLine("Unknown argument. Use 'A', 'B', or 'C'.\n");
            }
        }
        else
        {
            ShowScenarioA();
            Console.WriteLine(new string('-', 60));
            ShowScenarioB();
            Console.WriteLine(new string('-', 60));
            ShowScenarioC();
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("Demo complete.");
        }
    }

    // Scenario A: Unique Email Subscribers
    static void ShowScenarioA()
    {
        Console.WriteLine("Scenario A: Unique Email Subscribers (HashSet<string>)\n");
        Console.WriteLine("HashSet stores: email addresses (string) — use case: uniqueness + fast membership check\n");

        // Choose case-insensitive equality for emails
        var subscribers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        bool Subscribe(string email)
        {
            var normalized = NormalizeEmail(email);
            return subscribers.Add(normalized);
        }

        string NormalizeEmail(string email)
        {
            return email?.Trim() ?? string.Empty;
        }

        // Demo: adding duplicates
        Console.WriteLine("Demo: adding same email with different casing/spaces");
        Console.WriteLine("Subscribe('  Alex@Example.com ')");
        Console.WriteLine(Subscribe("  Alex@Example.com ") ? "Added" : "Already present");
        Console.WriteLine("Subscribe('alex@example.com')");
        Console.WriteLine(Subscribe("alex@example.com") ? "Added" : "Already present");

        Console.WriteLine($"Subscriber count: {subscribers.Count}");

        Console.WriteLine("Core API sketch:");
        Console.WriteLine("- bool Subscribe(string email) — adds normalized email; returns true if new");
        Console.WriteLine("- bool IsSubscribed(string email) — membership check (Contains)");
        Console.WriteLine("- int GetSubscriberCount() — Count\n");

        Console.WriteLine("Edge case: duplicate emails with different spacing/case — handled by normalization + case-insensitive comparer.");
    }

    // Scenario B: User Permissions & Roles
    static void ShowScenarioB()
    {
        Console.WriteLine("Scenario B: User Permissions & Roles\n");
        Console.WriteLine("HashSet stores: permission names (string) per user; roles map to HashSet<string> of permissions.\n");

        // Permissions are typically case-insensitive identifiers
        var userPermissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var rolePermissions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["reader"] = new HashSet<string>(new string []{"read"}, StringComparer.OrdinalIgnoreCase),
            ["editor"] = new HashSet<string>(new []{"read","write"}, StringComparer.OrdinalIgnoreCase)
        };

        void GrantPermission(string perm) => userPermissions.Add(perm.Trim());
        bool HasPermission(string perm) => userPermissions.Contains(perm.Trim()) || rolePermissions.Values.Any(r => r.Contains(perm.Trim()));
        var myHas = new HashSet<string>(new []{"wew", "wewew"}, StringComparer.OrdinalIgnoreCase);
        Console.WriteLine("Demo: grant 'write' and check 'read'/'write' presence (role-based and direct)");
        GrantPermission("write");
        Console.WriteLine($"Has read? {HasPermission("read")} (from roles?)");
        Console.WriteLine($"Has write? {HasPermission("write")} (direct grant)");

        Console.WriteLine("Core API sketch:");
        Console.WriteLine("- void GrantPermission(string userId, string permission)");
        Console.WriteLine("- void AssignRole(string userId, string role)");
        Console.WriteLine("- bool HasPermission(string userId, string permission) — fast membership checks via HashSet.Contains\n");

        Console.WriteLine("Edge case: permission name typos — consider normalizing and/or validating against canonical permission set when granting.");
    }

    // Scenario C: Spell-Checker Word List
    static void ShowScenarioC()
    {
        Console.WriteLine("Scenario C: Spell-Checker Word List\n");
        Console.WriteLine("HashSet stores: valid words (string), case-insensitive or normalized form.\n");

        // Small sample dictionary
        var validWords = new HashSet<string>(new [] {"hello","world","example","spell"}, StringComparer.OrdinalIgnoreCase);

        bool IsValidWord(string token)
        {
            var normalized = Normalize(token);
            return !string.IsNullOrEmpty(normalized) && validWords.Contains(normalized);
        }

        string Normalize(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return string.Empty;
            // simple punctuation stripping
            var chars = token.Where(char.IsLetterOrDigit).ToArray();
            return new string(chars).ToLowerInvariant();
        }

        var text = "Hello, wrld! This is an example. Hello";
        Console.WriteLine($"Text to check: {text}");

        var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                         .Select(t => Normalize(t))
                         .Where(t => !string.IsNullOrEmpty(t));

        var uniqueWords = new HashSet<string>(tokens, StringComparer.OrdinalIgnoreCase);
        var misspelled = new HashSet<string>(tokens.Where(t => !validWords.Contains(t)), StringComparer.OrdinalIgnoreCase);

        Console.WriteLine($"Unique words ({uniqueWords.Count}): {string.Join(", ", uniqueWords)}");
        Console.WriteLine($"Misspelled words ({misspelled.Count}): {string.Join(", ", misspelled)}");

        Console.WriteLine("Core API sketch:");
        Console.WriteLine("- bool IsValidWord(string word)");
        Console.WriteLine("- HashSet<string> GetMisspelledWords(IEnumerable<string> tokens)");
        Console.WriteLine("- HashSet<string> GetUniqueWords(IEnumerable<string> tokens)\n");

        Console.WriteLine("Edge case: punctuation attached to words — handled by simple normalization (strip punctuation) before membership checks.");
    }
}