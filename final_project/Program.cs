using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FinalProject.WorkOrders;

namespace FinalProject;

public static class Program
{
    private const string DataFilePath = "orders.txt";
    private static readonly Random Rng = new();
    private static WorkOrderRepository _repository = null!;
    private static readonly List<Contractor> Contractors = new()
    {
        new Contractor
        {
            Owner = "Andrew Baklinski",
            LegalName = "Baklinski Home Improvement LLC, DBA Baklinski Home Improvement",
            MailingAddress = "PO Box 1102, Mercer Island, WA 98040",
            Email = "joev@baklinski.com",
            Phone = "(206) 926-7916",
            LicenseNumber = "BAKLIHI801DJ"
        }
    };

    public static void Main()
    {
        _repository = new WorkOrderRepository(DataFilePath);
        _repository.Load();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Work Order Manager ===");
            Console.WriteLine("1. Add new work order");
            Console.WriteLine("2. List work orders");
            Console.WriteLine("3. Search work orders");
            Console.WriteLine("4. Update work order");
            Console.WriteLine("5. Delete work order");
            Console.WriteLine("6. Stats");
            Console.WriteLine("7. Generate invoice");
            Console.WriteLine("8. Unpaid balances by client");
            Console.WriteLine("9. Contractor data");
            Console.WriteLine("10. Save & Quit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddWorkOrder();
                    break;
                case "2":
                    ListWorkOrders();
                    break;
                case "3":
                    SearchWorkOrders();
                    break;
                case "4":
                    UpdateWorkOrder();
                    break;
                case "5":
                    DeleteWorkOrder();
                    break;
                case "6":
                    ShowStats();
                    break;
                case "7":
                    GenerateInvoice();
                    break;
                case "8":
                    ShowUnpaidBalancesByClient();
                    break;
                case "9":
                    ShowContractorDataMenu();
                    break;
                case "10":
                    _repository.Save();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static int GenerateUniqueOrderNumber()
    {
        while (true)
        {
            int num = Rng.Next(10_000_000, 100_000_000);
            if (!_repository.OrdersById.ContainsKey(num))
            {
                return num;
            }
        }
    }

    private static void AddWorkOrder()
    {
        Console.Clear();
        Console.WriteLine("=== Add Work Order ===");
        Console.WriteLine("(At any prompt, type 'q' to cancel and return to menu.)");
        Console.WriteLine();

        int orderNumber = GenerateUniqueOrderNumber();
        Console.WriteLine($"Generated order number: {orderNumber}");

        if (!TryReadDate("Enter work date (YYYY-MM-DD): ", out var workDate))
        {
            return;
        }

        if (!TryChooseFromSet(_repository.Clients, "Choose client (or enter new):", out var clientName))
        {
            return;
        }

        if (!TryChooseFromSet(_repository.Cities, "Choose city (or enter new):", out var city))
        {
            return;
        }

        if (!TryReadLine("Full address (street, city, state, ZIP): ", out var fullAddress))
        {
            return;
        }

        if (!TryReadLine("Phone number (digits only or any format): ", out var phone))
        {
            return;
        }

        if (!TryReadLine("Client email (optional): ", out var email))
        {
            return;
        }

        if (!TryReadDecimal("Materials amount (can be 0): ", out var materials))
        {
            return;
        }

        if (!TryReadDecimal("Payment amount: ", out var payment))
        {
            return;
        }

        if (!TryReadOptionalDate("Invoice date (YYYY-MM-DD) or leave empty: ", out var invoiceDate))
        {
            return;
        }

        if (!TryReadOptionalDate("Payment date (YYYY-MM-DD) or leave empty: ", out var paymentDate))
        {
            return;
        }

        if (!TryAskPaymentMethod(out var paymentMethod))
        {
            return;
        }

        if (!TryReadLine("Description: ", out var description))
        {
            return;
        }

        if (!TryReadDouble("Hours worked: ", out var hoursWorked))
        {
            return;
        }

        if (!TryReadYesNo("Is sales tax included? (y/n): ", out var isTaxIncluded))
        {
            return;
        }

        if (!TryReadYesNo("Is this order paid? (y/n): ", out var isPaid))
        {
            return;
        }

        var order = new WorkOrder
        {
            OrderNumber = orderNumber,
            WorkDate = workDate,
            ClientName = clientName,
            City = city,
            FullAddress = fullAddress,
            PhoneNumber = phone,
            Email = email,
            MaterialsAmount = materials,
            PaymentAmount = payment,
            InvoiceDate = invoiceDate,
            PaymentDate = paymentDate,
            PaymentMethod = paymentMethod,
            Description = description,
            HoursWorked = hoursWorked,
            IsSalesTaxIncluded = isTaxIncluded,
            IsPaid = isPaid
        };

        _repository.Add(order);

        Console.WriteLine("Work order added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static void ListWorkOrders()
    {
        Console.Clear();
        Console.WriteLine("=== List Work Orders ===");

        var list = _repository.GetAllAsList();
        if (list.Count == 0)
        {
            Console.WriteLine("No work orders.");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Sort by:");
        Console.WriteLine("1. Order date");
        Console.WriteLine("2. Payment amount");
        Console.WriteLine("3. Client name");
        Console.WriteLine("4. City");
        Console.WriteLine("5. Order date, unpaid first");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                list.Sort((a, b) => a.WorkDate.CompareTo(b.WorkDate));
                break;
            case "2":
                list.Sort((a, b) => a.PaymentAmount.CompareTo(b.PaymentAmount));
                break;
            case "3":
                list.Sort((a, b) => string.Compare(a.ClientName, b.ClientName, StringComparison.OrdinalIgnoreCase));
                break;
            case "4":
                list.Sort((a, b) => string.Compare(a.City, b.City, StringComparison.OrdinalIgnoreCase));
                break;
            case "5":
                list.Sort((a, b) =>
                {
                    int paidCompare = a.IsPaid.CompareTo(b.IsPaid); // unpaid (false) first
                    if (paidCompare != 0) return paidCompare;
                    return a.WorkDate.CompareTo(b.WorkDate);
                });
                break;
            default:
                Console.WriteLine("Unknown choice, listing unsorted.");
                break;
        }

        Console.WriteLine();
        Console.WriteLine("Order      Date         Client               City               PayAmt      Paid  PaymentMethod      Hours   Phone           Email");
        Console.WriteLine(new string('-', 150));
        decimal totalAmount = 0m;
        foreach (var o in list)
        {
            var paidFlag = o.IsPaid ? "Y" : "N";
            totalAmount += o.PaymentAmount;
            Console.WriteLine(
            $"{o.OrderNumber,10} {o.WorkDate:yyyy-MM-dd}  {Truncate(o.ClientName,20),-20} {Truncate(o.City,18),-18} {o.PaymentAmount,10:C2}   {paidFlag,3}   {Truncate(o.PaymentMethod.ToString(),16),-16} {o.HoursWorked,7:F1} {Truncate(o.PhoneNumber,14),-14} {Truncate(o.Email,22),-22}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total for all listed orders: {totalAmount:C2}");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void SearchWorkOrders()
    {
        Console.Clear();
        Console.WriteLine("=== Search Work Orders ===");
        Console.WriteLine("1. By order number");
        Console.WriteLine("2. By client name");
        Console.WriteLine("3. By city");
        Console.WriteLine("4. By date range");
        Console.WriteLine("5. By date range (unpaid only)");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        List<WorkOrder> results;

        switch (choice)
        {
            case "1":
                if (!TryReadInt("Enter order number (or 'q' to cancel): ", out var id))
                {
                    return;
                }

                if (_repository.TryGet(id, out var orderById) && orderById != null)
                {
                    PrintOrderDetails(orderById);
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
                break;
            case "2":
                Console.Write("Enter client name (part or full): ");
                var client = Console.ReadLine() ?? string.Empty;
                results = _repository
                    .GetAllAsList()
                    .FindAll(o => o.ClientName.Contains(client, StringComparison.OrdinalIgnoreCase));
                ShowSearchResultsWithTotals(results, groupByClient: true);
                break;
            case "3":
                Console.Write("Enter city (part or full): ");
                var city = Console.ReadLine() ?? string.Empty;
                results = _repository
                    .GetAllAsList()
                    .FindAll(o => o.City.Contains(city, StringComparison.OrdinalIgnoreCase));
                ShowSearchResultsWithTotals(results, groupByClient: true);
                break;
            case "4":
                Console.WriteLine("Enter date range (inclusive).");
                var from = ReadDate("From date (YYYY-MM-DD): ");
                var to = ReadDate("To date (YYYY-MM-DD): ");
                results = _repository
                    .GetAllAsList()
                    .FindAll(o => o.WorkDate.Date >= from.Date && o.WorkDate.Date <= to.Date);
                ShowSearchResultsWithTotals(results, groupByClient: false);
                break;
            case "5":
                Console.WriteLine("Enter date range (inclusive) for unpaid orders.");
                var fromU = ReadDate("From date (YYYY-MM-DD): ");
                var toU = ReadDate("To date (YYYY-MM-DD): ");
                results = _repository
                    .GetAllAsList()
                    .FindAll(o => !o.IsPaid && o.WorkDate.Date >= fromU.Date && o.WorkDate.Date <= toU.Date);
                ShowSearchResultsWithTotals(results, groupByClient: false);
                break;
            default:
                Console.WriteLine("Unknown choice.");
                break;
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void UpdateWorkOrder()
    {
        Console.Clear();
        Console.WriteLine("=== Update Work Order ===");
        if (!TryReadInt("Enter order number (or 'q' to cancel): ", out var id))
        {
            return;
        }
        if (!_repository.TryGet(id, out var order) || order == null)
        {
            Console.WriteLine("Order not found. Press any key...");
            Console.ReadKey();
            return;
        }

        PrintOrderDetails(order);
        Console.WriteLine();
        Console.WriteLine("What do you want to update?");
        Console.WriteLine("1. Work date");
        Console.WriteLine("2. Client name");
        Console.WriteLine("3. City");
        Console.WriteLine("4. Materials amount");
        Console.WriteLine("5. Payment amount");
        Console.WriteLine("6. Invoice date");
        Console.WriteLine("7. Payment date");
        Console.WriteLine("8. Payment method");
        Console.WriteLine("9. Description");
        Console.WriteLine("10. Hours worked");
        Console.WriteLine("11. Sales tax included");
        Console.WriteLine("12. Cancel");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                order.WorkDate = ReadDate("New work date: ");
                break;
            case "2":
                Console.Write("New client name: ");
                order.ClientName = Console.ReadLine() ?? string.Empty;
                break;
            case "3":
                Console.Write("New city: ");
                order.City = Console.ReadLine() ?? string.Empty;
                break;
            case "4":
                order.MaterialsAmount = ReadDecimal("New materials amount: ");
                break;
            case "5":
                order.PaymentAmount = ReadDecimal("New payment amount: ");
                break;
            case "6":
                order.InvoiceDate = ReadOptionalDate("New invoice date (or empty): ");
                break;
            case "7":
                order.PaymentDate = ReadOptionalDate("New payment date (or empty): ");
                break;
            case "8":
                order.PaymentMethod = AskPaymentMethod();
                break;
            case "9":
                Console.Write("New description: ");
                order.Description = Console.ReadLine() ?? string.Empty;
                break;
            case "10":
                order.HoursWorked = ReadDouble("New hours worked: ");
                break;
            case "11":
                order.IsSalesTaxIncluded = ReadYesNo("Is sales tax included? (y/n): ");
                break;
            case "12":
                Console.WriteLine("Cancelled.");
                break;
            default:
                Console.WriteLine("Unknown choice.");
                break;
        }

        Console.WriteLine("Updated. Press any key...");
        Console.ReadKey();
    }

    private static void DeleteWorkOrder()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Work Order ===");
        if (!TryReadInt("Enter order number (or 'q' to cancel): ", out var id))
        {
            return;
        }

        if (_repository.Remove(id))
        {
            Console.WriteLine("Deleted successfully.");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void ShowStats()
    {
        Console.Clear();
        Console.WriteLine("=== Stats ===");
        var all = _repository.GetAllAsList();
        int total = all.Count;
        decimal totalPayments = 0m;
        double totalHours = 0;

        var byCity = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var o in all)
        {
            totalPayments += o.PaymentAmount;
            totalHours += o.HoursWorked;

            if (!byCity.ContainsKey(o.City))
            {
                byCity[o.City] = 0;
            }
            byCity[o.City]++;
        }

        Console.WriteLine($"Total orders: {total}");
        Console.WriteLine($"Total payment amount: {totalPayments:C}");
        Console.WriteLine($"Total hours worked: {totalHours:F1}");
        Console.WriteLine();
        Console.WriteLine("Orders by city:");
        foreach (var kvp in byCity.OrderBy(k => k.Key))
        {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void ShowUnpaidBalancesByClient()
    {
        Console.Clear();
        Console.WriteLine("=== Unpaid Balances by Client ===");

        var all = _repository.GetAllAsList();
        var groups = all
            .Where(o => !o.IsPaid)
            .GroupBy(o => o.ClientName)
            .Select(g => new
            {
                Client = g.Key,
                Count = g.Count(),
                TotalDue = g.Sum(o => o.PaymentAmount),
                OrderNumbers = g.Select(o => o.OrderNumber).OrderBy(n => n).ToList()
            })
            .Where(x => x.Count > 0 && x.TotalDue > 0)
            .OrderByDescending(x => x.TotalDue)
            .ThenBy(x => x.Client, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (groups.Count == 0)
        {
            Console.WriteLine("No unpaid balances. Great job!");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Client                              | Orders |   Total Due | Order Numbers");
            Console.WriteLine(new string('-', 95));

            foreach (var g in groups)
            {
                var ordersText = string.Join(", ", g.OrderNumbers);
                Console.WriteLine(
                    $"{Truncate(g.Client,30),-30} | {g.Count,6} | {g.TotalDue,11:C0} | {ordersText}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void GenerateInvoice()
    {
        Console.Clear();
        Console.WriteLine("=== Generate Invoice ===");
        if (!TryReadInt("Enter order number (or 'q' to cancel): ", out var id))
        {
            return;
        }

        if (_repository.TryGet(id, out var order) && order != null)
        {
            var contractor = FindContractorForClient(order.ClientName);
            InvoiceRenderer.SaveInvoiceToFile(order);
            var htmlPath = InvoiceRenderer.SaveHtmlInvoiceToFile(order, contractor);
            Console.WriteLine("Text invoice saved in 'invoices' folder.");
            Console.WriteLine("HTML invoice saved as:");
            Console.WriteLine(htmlPath);
            Console.WriteLine("Open this file in your browser to review, print or save as PDF.");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    // --- Helper methods ---

    private static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength - 1) + "…";
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value))
            {
                return value;
            }
            Console.WriteLine("Invalid number, try again.");
        }
    }

    private static bool TryReadInt(string prompt, out int value)
    {
        value = 0;
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (int.TryParse(input, out value))
            {
                return true;
            }

            Console.WriteLine("Invalid number, try again or type 'q' to cancel.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }
            Console.WriteLine("Invalid number, try again (use '.' as decimal separator).");
        }
    }

    private static bool TryReadDouble(string prompt, out double value)
    {
        value = 0;
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                return true;
            }

            Console.WriteLine("Invalid number, try again (use '.' as decimal separator) or type 'q' to cancel.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }
            Console.WriteLine("Invalid amount, try again (use '.' as decimal separator).");
        }
    }

    private static bool TryReadDecimal(string prompt, out decimal value)
    {
        value = 0;
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                return true;
            }

            Console.WriteLine("Invalid amount, try again (use '.' as decimal separator) or type 'q' to cancel.");
        }
    }

    private static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date.Date;
            }
            Console.WriteLine("Invalid date, expected format YYYY-MM-DD.");
        }
    }

    private static bool TryReadDate(string prompt, out DateTime value)
    {
        value = DateTime.MinValue;
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                value = date.Date;
                return true;
            }

            Console.WriteLine("Invalid date, expected format YYYY-MM-DD or 'q' to cancel.");
        }
    }

    private static DateTime? ReadOptionalDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date.Date;
            }
            Console.WriteLine("Invalid date, expected format YYYY-MM-DD or empty.");
        }
    }

    private static bool TryReadOptionalDate(string prompt, out DateTime? value)
    {
        value = null;
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                value = null;
                return true;
            }

            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                value = date.Date;
                return true;
            }

            Console.WriteLine("Invalid date, expected format YYYY-MM-DD, empty, or 'q' to cancel.");
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();
            if (input == "y" || input == "yes") return true;
            if (input == "n" || input == "no") return false;
            Console.WriteLine("Please enter 'y' or 'n'.");
        }
    }

    private static bool TryReadYesNo(string prompt, out bool value)
    {
        value = false;
        while (true)
        {
            Console.Write(prompt);
            var inputRaw = Console.ReadLine();

            if (string.Equals(inputRaw, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var input = (inputRaw ?? string.Empty).Trim().ToLowerInvariant();
            if (input == "y" || input == "yes")
            {
                value = true;
                return true;
            }
            if (input == "n" || input == "no")
            {
                value = false;
                return true;
            }

            Console.WriteLine("Please enter 'y' or 'n', or 'q' to cancel.");
        }
    }

    private static PaymentMethod AskPaymentMethod()
    {
        Console.WriteLine("Choose payment method:");
        var values = Enum.GetValues<PaymentMethod>()
            .Where(m => m != PaymentMethod.Unknown)
            .ToList();

        for (int i = 0; i < values.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {values[i]}");
        }

        while (true)
        {
            Console.Write("Your choice: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var idx) && idx >= 1 && idx <= values.Count)
            {
                return values[idx - 1];
            }
            Console.WriteLine("Invalid choice, try again.");
        }
    }

    private static bool TryAskPaymentMethod(out PaymentMethod method)
    {
        method = PaymentMethod.Unknown;

        Console.WriteLine("Choose payment method:");
        var values = Enum.GetValues<PaymentMethod>()
            .Where(m => m != PaymentMethod.Unknown)
            .ToList();

        for (int i = 0; i < values.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {values[i]}");
        }

        while (true)
        {
            Console.Write("Your choice (or 'q' to cancel): ");
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (int.TryParse(input, out var idx) && idx >= 1 && idx <= values.Count)
            {
                method = values[idx - 1];
                return true;
            }
            Console.WriteLine("Invalid choice, try again or 'q' to cancel.");
        }
    }

    private static string ChooseFromSet(HashSet<string> set, string prompt)
    {
        var list = set.OrderBy(x => x).ToList();
        Console.WriteLine(prompt);

        if (list.Count == 0)
        {
            Console.Write("Enter value: ");
            return Console.ReadLine() ?? string.Empty;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {list[i]}");
        }
        Console.WriteLine($"{list.Count + 1}. Enter new value");

        while (true)
        {
            Console.Write("Your choice: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var idx))
            {
                if (idx >= 1 && idx <= list.Count)
                {
                    return list[idx - 1];
                }
                if (idx == list.Count + 1)
                {
                    Console.Write("Enter new value: ");
                    return Console.ReadLine() ?? string.Empty;
                }
            }
            Console.WriteLine("Invalid choice, try again.");
        }
    }

    private static bool TryChooseFromSet(HashSet<string> set, string prompt, out string value)
    {
        value = string.Empty;

        var list = set.OrderBy(x => x).ToList();
        Console.WriteLine(prompt);

        if (list.Count == 0)
        {
            while (true)
            {
                Console.Write("Enter value (or 'q' to cancel): ");
                var input = Console.ReadLine();
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(input))
                {
                    value = input!;
                    return true;
                }
                Console.WriteLine("Value cannot be empty.");
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {list[i]}");
        }
        Console.WriteLine($"{list.Count + 1}. Enter new value");

        while (true)
        {
            Console.Write("Your choice (or 'q' to cancel): ");
            var input = Console.ReadLine();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (int.TryParse(input, out var idx))
            {
                if (idx >= 1 && idx <= list.Count)
                {
                    value = list[idx - 1];
                    return true;
                }
                if (idx == list.Count + 1)
                {
                    while (true)
                    {
                        Console.Write("Enter new value (or 'q' to cancel): ");
                        var newVal = Console.ReadLine();
                        if (string.Equals(newVal, "q", StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }
                        if (!string.IsNullOrWhiteSpace(newVal))
                        {
                            value = newVal!;
                            return true;
                        }
                        Console.WriteLine("Value cannot be empty.");
                    }
                }
            }
            Console.WriteLine("Invalid choice, try again.");
        }
    }

    private static bool TryReadLine(string prompt, out string value)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
        {
            value = string.Empty;
            return false;
        }

        value = input ?? string.Empty;
        return true;
    }

    private static void PrintOrderDetails(WorkOrder o)
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Order #: {o.OrderNumber}");
        Console.WriteLine($"Work date: {o.WorkDate:yyyy-MM-dd}");
        Console.WriteLine($"Client: {o.ClientName}");
        Console.WriteLine($"Email: {o.Email}");
        Console.WriteLine($"Phone: {o.PhoneNumber}");
        Console.WriteLine($"City: {o.City}");
        Console.WriteLine($"Full address: {o.FullAddress}");
        Console.WriteLine($"Materials: {o.MaterialsAmount:C}");
        Console.WriteLine($"Payment: {o.PaymentAmount:C}");
        Console.WriteLine($"Invoice date: {o.InvoiceDate:yyyy-MM-dd}");
        Console.WriteLine($"Payment date: {o.PaymentDate:yyyy-MM-dd}");
        Console.WriteLine($"Payment method: {o.PaymentMethod}");
        Console.WriteLine($"Hours worked: {o.HoursWorked:F1}");
        Console.WriteLine($"Sales tax included: {(o.IsSalesTaxIncluded ? "Yes" : "No")}");
        Console.WriteLine($"Paid: {(o.IsPaid ? "Yes" : "No")}");
        Console.WriteLine($"Description: {o.Description}");
        var contractor = FindContractorForClient(o.ClientName);
        if (contractor != null)
        {
            Console.WriteLine();
            Console.WriteLine("--- Contractor details ---");
            Console.WriteLine($"Owner: {contractor.Owner}");
            Console.WriteLine($"Legal name: {contractor.LegalName}");
            Console.WriteLine($"Mailing address: {contractor.MailingAddress}");
            Console.WriteLine($"Contact email: {contractor.Email}");
            Console.WriteLine($"Phone: {contractor.Phone}");
            Console.WriteLine($"License: {contractor.LicenseNumber}");
        }
        Console.WriteLine("------------------------------");
    }

    private static void ShowContractorDataMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Contractor Data ===");
            Console.WriteLine("1. List contractors");
            Console.WriteLine("2. Add contractor");
            Console.WriteLine("3. Find contractor");
            Console.WriteLine("4. Edit contractor");
            Console.WriteLine("5. Delete contractor");
            Console.WriteLine("6. Back to main menu");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListContractors();
                    break;
                case "2":
                    AddContractor();
                    break;
                case "3":
                    FindContractorMenu();
                    break;
                case "4":
                    EditContractor();
                    break;
                case "5":
                    DeleteContractor();
                    break;
                case "6":
                case "q":
                case "Q":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListContractors()
    {
        Console.Clear();
        Console.WriteLine("=== Contractors ===");
        if (Contractors.Count == 0)
        {
            Console.WriteLine("No contractors defined.");
        }
        else
        {
            Console.WriteLine("#  Short / Legal Name");
            Console.WriteLine(new string('-', 60));
            for (int i = 0; i < Contractors.Count; i++)
            {
                var c = Contractors[i];
                Console.WriteLine($"{i + 1,2}. {c.LegalName}");
                Console.WriteLine($"    Owner: {c.Owner}, Phone: {c.Phone}, License: {c.LicenseNumber}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void AddContractor()
    {
        Console.Clear();
        Console.WriteLine("=== Add Contractor ===");

        Console.Write("Owner: ");
        var owner = Console.ReadLine() ?? string.Empty;
        Console.Write("Legal name: ");
        var legalName = Console.ReadLine() ?? string.Empty;
        Console.Write("Mailing address: ");
        var address = Console.ReadLine() ?? string.Empty;
        Console.Write("Email: ");
        var email = Console.ReadLine() ?? string.Empty;
        Console.Write("Phone: ");
        var phone = Console.ReadLine() ?? string.Empty;
        Console.Write("License number: ");
        var license = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(legalName))
        {
            Console.WriteLine("Legal name is required. Press any key...");
            Console.ReadKey();
            return;
        }

        Contractors.Add(new Contractor
        {
            Owner = owner,
            LegalName = legalName,
            MailingAddress = address,
            Email = email,
            Phone = phone,
            LicenseNumber = license
        });

        Console.WriteLine("Contractor added. Press any key...");
        Console.ReadKey();
    }

    private static void FindContractorMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Find Contractor ===");
        Console.Write("Enter part of legal name or license: ");
        var term = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(term))
        {
            Console.WriteLine("Empty search term. Press any key...");
            Console.ReadKey();
            return;
        }

        var matches = Contractors
            .Where(c => c.LegalName.Contains(term, StringComparison.OrdinalIgnoreCase)
                        || c.LicenseNumber.Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matches.Count == 0)
        {
            Console.WriteLine("No contractors found.");
        }
        else
        {
            Console.WriteLine();
            foreach (var c in matches)
            {
                PrintContractorCard(c);
                Console.WriteLine();
            }
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void EditContractor()
    {
        if (Contractors.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("No contractors to edit. Press any key...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("=== Edit Contractor ===");
        for (int i = 0; i < Contractors.Count; i++)
        {
            Console.WriteLine($"{i + 1,2}. {Contractors[i].LegalName}");
        }
        Console.Write("Select number to edit: ");
        if (!int.TryParse(Console.ReadLine(), out var index) || index < 1 || index > Contractors.Count)
        {
            Console.WriteLine("Invalid selection. Press any key...");
            Console.ReadKey();
            return;
        }

        var c = Contractors[index - 1];

        Console.WriteLine("Leave field empty to keep current value.");
        Console.Write($"Owner ({c.Owner}): ");
        var owner = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(owner)) c.Owner = owner;

        Console.Write($"Legal name ({c.LegalName}): ");
        var legalName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(legalName)) c.LegalName = legalName;

        Console.Write($"Mailing address ({c.MailingAddress}): ");
        var addr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(addr)) c.MailingAddress = addr;

        Console.Write($"Email ({c.Email}): ");
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email)) c.Email = email;

        Console.Write($"Phone ({c.Phone}): ");
        var phone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phone)) c.Phone = phone;

        Console.Write($"License number ({c.LicenseNumber}): ");
        var license = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(license)) c.LicenseNumber = license;

        Console.WriteLine("Contractor updated. Press any key...");
        Console.ReadKey();
    }

    private static void DeleteContractor()
    {
        if (Contractors.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("No contractors to delete. Press any key...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("=== Delete Contractor ===");
        for (int i = 0; i < Contractors.Count; i++)
        {
            Console.WriteLine($"{i + 1,2}. {Contractors[i].LegalName}");
        }
        Console.Write("Select number to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var index) || index < 1 || index > Contractors.Count)
        {
            Console.WriteLine("Invalid selection. Press any key...");
            Console.ReadKey();
            return;
        }

        var removed = Contractors[index - 1];
        Contractors.RemoveAt(index - 1);
        Console.WriteLine($"Deleted contractor: {removed.LegalName}");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static Contractor? FindContractorForClient(string clientName)
    {
        if (string.IsNullOrWhiteSpace(clientName)) return null;

        return Contractors.FirstOrDefault(c =>
            clientName.Contains(c.LegalName, StringComparison.OrdinalIgnoreCase) ||
            c.LegalName.Contains(clientName, StringComparison.OrdinalIgnoreCase));
    }

    private static void PrintContractorCard(Contractor c)
    {
        Console.WriteLine($"Owner: {c.Owner}");
        Console.WriteLine($"Legal name: {c.LegalName}");
        Console.WriteLine($"Mailing address: {c.MailingAddress}");
        Console.WriteLine($"Contact email: {c.Email}");
        Console.WriteLine($"Phone: {c.Phone}");
        Console.WriteLine($"License: {c.LicenseNumber}");
    }

    private static void PrintOrderList(List<WorkOrder> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("No matching orders.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Order  | Date       | Client           | City           | PayAmt  | Hours | Email");
        Console.WriteLine(new string('-', 110));
        foreach (var o in list)
        {
            Console.WriteLine(
            $"{o.OrderNumber,6} | {o.WorkDate:yyyy-MM-dd} | {Truncate(o.ClientName,15),-15} | {Truncate(o.City,15),-15} | {o.PaymentAmount,7:C0} | {o.HoursWorked,5:F1} | {Truncate(o.Email,30),-30}");
        }
    }

    private static void ShowSearchResultsWithTotals(List<WorkOrder> results, bool groupByClient)
    {
        if (results.Count == 0)
        {
            Console.WriteLine("No matching orders.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Sort results by:");
        Console.WriteLine("1. Work date");
        Console.WriteLine("2. Payment amount");
        Console.WriteLine("3. Client name");
        Console.WriteLine("4. City");
        Console.WriteLine("5. Unpaid first (then by date)");
        Console.Write("Choice (Enter for default by date): ");
        var sortChoice = Console.ReadLine();

        switch (sortChoice)
        {
            case "2":
                results.Sort((a, b) => a.PaymentAmount.CompareTo(b.PaymentAmount));
                break;
            case "3":
                results.Sort((a, b) => string.Compare(a.ClientName, b.ClientName, StringComparison.OrdinalIgnoreCase));
                break;
            case "4":
                results.Sort((a, b) => string.Compare(a.City, b.City, StringComparison.OrdinalIgnoreCase));
                break;
            case "5":
                results.Sort((a, b) =>
                {
                    int paidCompare = a.IsPaid.CompareTo(b.IsPaid); // false (0) before true (1)
                    if (paidCompare != 0) return paidCompare;
                    return a.WorkDate.CompareTo(b.WorkDate);
                });
                break;
            default:
                results.Sort((a, b) => a.WorkDate.CompareTo(b.WorkDate));
                break;
        }

        // Print list with unpaid clearly marked
        Console.WriteLine();
        Console.WriteLine("Order  | Date       | Client           | City           | PayAmt  | Paid | Hours | Email");
        Console.WriteLine(new string('-', 120));
        decimal totalAmount = 0m;
        foreach (var o in results)
        {
            var paidFlag = o.IsPaid ? "Y" : "N";
            totalAmount += o.PaymentAmount;
            Console.WriteLine(
                $"{o.OrderNumber,6} | {o.WorkDate:yyyy-MM-dd} | {Truncate(o.ClientName,15),-15} | {Truncate(o.City,15),-15} | {o.PaymentAmount,7:C0} | {paidFlag,4} | {o.HoursWorked,5:F1} | {Truncate(o.Email,30),-30}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total for all matching orders: {totalAmount:C0}");

        if (!groupByClient)
        {
            return;
        }

        // Totals per client
        Console.WriteLine();
        Console.WriteLine("Per-client totals:");
        var groups = results
            .GroupBy(o => o.ClientName)
            .OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase);

        foreach (var g in groups)
        {
            decimal totalAll = g.Sum(o => o.PaymentAmount);
            decimal totalUnpaid = g.Where(o => !o.IsPaid).Sum(o => o.PaymentAmount);
            int countAll = g.Count();
            int countUnpaid = g.Count(o => !o.IsPaid);

            Console.WriteLine($"- {g.Key}: {countAll} orders, total {totalAll:C0}; unpaid {countUnpaid} orders, {totalUnpaid:C0}");
        }
    }
}
