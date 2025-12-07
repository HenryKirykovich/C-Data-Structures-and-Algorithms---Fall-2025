using System;
using System.IO;
using System.Text;

namespace FinalProject.WorkOrders;

public static class InvoiceRenderer
{
    // Predefined combined sales tax rates for Washington cities used in orders
    // Values below come from official published state/local combined rates.
    private static decimal GetSalesTaxRateForCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city)) return 0.10m; // fallback default

        switch (city.Trim().ToLowerInvariant())
        {
            // King County
            case "bellevue":
                return 0.1020m;
            case "seattle":
                return 0.1035m;
            case "kirkland":
                return 0.1030m;
            case "sammamish":
                return 0.1020m;
            case "issaquah":
                return 0.1030m;
            case "mercer island":
                return 0.1020m;
            case "renton":
                return 0.1030m;

            // Snohomish County
            case "lynnwood":
                return 0.1060m;
            case "everett":
                return 0.0990m;
            case "snohomish":
                return 0.0930m;

            default:
                return 0.10m;
        }
    }

    public static string RenderTextInvoice(WorkOrder order)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== INVOICE ===");
        sb.AppendLine($"Order #: {order.OrderNumber}");
        sb.AppendLine($"Date: {order.WorkDate:yyyy-MM-dd}");
        sb.AppendLine($"Client: {order.ClientName}");
        sb.AppendLine($"City: {order.City}");
        sb.AppendLine();
        sb.AppendLine($"Description: {order.Description}");
        sb.AppendLine($"Hours worked: {order.HoursWorked:F2}");
        sb.AppendLine($"Materials amount: {order.MaterialsAmount:C}");
        sb.AppendLine($"Payment amount: {order.PaymentAmount:C}");
        sb.AppendLine($"Sales tax included: {(order.IsSalesTaxIncluded ? "Yes" : "No")}");
        sb.AppendLine($"Invoice date: {order.InvoiceDate:yyyy-MM-dd}");
        sb.AppendLine($"Payment date: {order.PaymentDate:yyyy-MM-dd}");
        sb.AppendLine($"Payment method: {order.PaymentMethod}");
        sb.AppendLine("================");
        return sb.ToString();
    }

    public static void SaveInvoiceToFile(WorkOrder order, string directory = "invoices")
    {
        Directory.CreateDirectory(directory);
        string invoiceDate = (order.InvoiceDate ?? DateTime.Today).ToString("yyyyMMdd");
        string fileName = Path.Combine(directory, $"invoice_{invoiceDate}_{order.OrderNumber}.txt");
        string content = RenderTextInvoice(order);
        File.WriteAllText(fileName, content);
    }

    public static string RenderHtmlInvoice(WorkOrder order, Contractor? contractor)
    {
        // Derive simple labor/material split for display
        decimal laborAmount = order.PaymentAmount - order.MaterialsAmount;
        if (laborAmount < 0) laborAmount = order.PaymentAmount;

        string invoiceDate = (order.InvoiceDate ?? DateTime.Today).ToString("yyyy-MM-dd");

        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("  <meta charset=\"utf-8\" />");
        sb.AppendLine("  <title>Invoice " + order.OrderNumber + "</title>");
        sb.AppendLine("  <style>");
        sb.AppendLine("    body { font-family: Arial, sans-serif; margin: 20px; }");
        sb.AppendLine("    .header { display: flex; justify-content: space-between; align-items: flex-start; }");
        sb.AppendLine("    .contractor-name { font-size: 22px; font-weight: bold; }");
        sb.AppendLine("    .section { margin-top: 20px; }");
        sb.AppendLine("    .section-title { font-weight: bold; margin-bottom: 5px; }");
        sb.AppendLine("    table { border-collapse: collapse; width: 100%; margin-top: 10px; }");
        sb.AppendLine("    th, td { border: 1px solid #ccc; padding: 6px 8px; font-size: 13px; }");
        sb.AppendLine("    th { background-color: #f0f0f0; text-align: left; }");
        sb.AppendLine("    .right { text-align: right; }");
        sb.AppendLine("    .totals-table { width: 50%; margin-left: auto; }");
        sb.AppendLine("  </style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");

        sb.AppendLine("  <div class=\"header\">");
        sb.AppendLine("    <div>");
        var c = contractor;
        if (c != null)
        {
            sb.AppendLine("      <div class=\"contractor-name\">" + Escape(c.LegalName) + "</div>");
            sb.AppendLine("      <div>Owner: " + Escape(c.Owner) + "</div>");
            sb.AppendLine("      <div>Business Address: " + Escape(c.MailingAddress) + "</div>");
            sb.AppendLine("      <div>WA Contractor Registration: " + Escape(c.LicenseNumber) + "</div>");
            sb.AppendLine("      <div>Email: " + Escape(c.Email) + "</div>");
            sb.AppendLine("      <div>Phone: " + Escape(c.Phone) + "</div>");
        }
        else
        {
            sb.AppendLine("      <div class=\"contractor-name\">Smart &amp; Handy Repairs</div>");
        }
        sb.AppendLine("    </div>");

        sb.AppendLine("    <div>");
        sb.AppendLine("      <h2>INVOICE</h2>");
        sb.AppendLine("      <div>Invoice #: " + order.OrderNumber + "</div>");
        sb.AppendLine("      <div>Invoice date: " + invoiceDate + "</div>");
        sb.AppendLine("      <div>Work date: " + order.WorkDate.ToString("yyyy-MM-dd") + "</div>");
        sb.AppendLine("    </div>");
        sb.AppendLine("  </div>");

        sb.AppendLine("  <div class=\"section\">");
        sb.AppendLine("    <div class=\"section-title\">Bill To</div>");
        var clientLine = Escape(order.ClientName);
        if (!string.IsNullOrWhiteSpace(order.Email))
        {
            clientLine += " (" + Escape(order.Email) + ")";
        }
        sb.AppendLine("    <div>" + clientLine + "</div>");
        if (!string.IsNullOrWhiteSpace(order.FullAddress))
        {
            sb.AppendLine("    <div>" + Escape(order.FullAddress) + "</div>");
        }
        if (!string.IsNullOrWhiteSpace(order.PhoneNumber))
        {
            sb.AppendLine("    <div>Phone: " + Escape(order.PhoneNumber) + "</div>");
        }
        sb.AppendLine("  </div>");

        sb.AppendLine("  <div class=\"section\">");
        sb.AppendLine("    <div class=\"section-title\">Work Description</div>");
        sb.AppendLine("    <table>");
        sb.AppendLine("      <tr><th>Description</th><th class=\"right\">Hours</th><th class=\"right\">Labor</th><th class=\"right\">Materials</th><th class=\"right\">Line Total</th></tr>");
        sb.AppendLine("      <tr>");
        var desc = Escape(order.Description);
        if (!string.IsNullOrWhiteSpace(order.City))
        {
            desc += " (" + Escape(order.City) + ")";
        }
        sb.AppendLine("        <td>" + desc + "</td>");
        sb.AppendLine("        <td class=\"right\">" + order.HoursWorked.ToString("F2") + "</td>");
        sb.AppendLine("        <td class=\"right\">" + laborAmount.ToString("C") + "</td>");
        sb.AppendLine("        <td class=\"right\">" + order.MaterialsAmount.ToString("C") + "</td>");
        sb.AppendLine("        <td class=\"right\">" + order.PaymentAmount.ToString("C") + "</td>");
        sb.AppendLine("      </tr>");
        sb.AppendLine("    </table>");
        sb.AppendLine("  </div>");

        sb.AppendLine("  <div class=\"section\">");
        sb.AppendLine("    <table class=\"totals-table\">");
        var taxRate = GetSalesTaxRateForCity(order.City);
        string taxRatePercent = (taxRate * 100m).ToString("0.00");
        var taxIncludedText = order.IsSalesTaxIncluded ? "Yes" : "No";
        sb.AppendLine("      <tr><th>Subtotal</th><td class=\"right\">" + order.PaymentAmount.ToString("C") + "</td></tr>");
        sb.AppendLine("      <tr><th>Sales tax included</th><td class=\"right\">" + taxIncludedText + " (" + taxRatePercent + "%)" + "</td></tr>");
        sb.AppendLine("      <tr><th>Total</th><td class=\"right\">" + order.PaymentAmount.ToString("C") + "</td></tr>");
        sb.AppendLine("    </table>");
        sb.AppendLine("  </div>");

        sb.AppendLine("  <div class=\"section\">");
        sb.AppendLine("    <div class=\"section-title\">Payment Options</div>");
        sb.AppendLine("    <div>1. Check: Payable to Smart &amp; Handy Repairs</div>");
        sb.AppendLine("    <div>2. Zelle: +1 425-221-4422</div>");
        sb.AppendLine("    <div>3. Bank Transfer (ACH / Wire):</div>");
        sb.AppendLine("    <div>Bank: US Bank</div>");
        sb.AppendLine("    <div>Account Name: Smart &amp; Handy Repairs (Checking)</div>");
        sb.AppendLine("    <div>Routing Number: 125000105</div>");
        sb.AppendLine("    <div>Account Number: 168405062899</div>");
        sb.AppendLine("  </div>");

        sb.AppendLine("  <div class=\"section\">");
        sb.AppendLine("    <div class=\"section-title\">Notes / Terms</div>");
        sb.AppendLine("    <div>Please contact me if you have any questions regarding payment.</div>");
        sb.AppendLine("  </div>");

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    public static string SaveHtmlInvoiceToFile(WorkOrder order, Contractor? contractor, string directory = "invoices")
    {
        Directory.CreateDirectory(directory);
        string invoiceDate = (order.InvoiceDate ?? DateTime.Today).ToString("yyyyMMdd");
        string fileName = Path.Combine(directory, $"invoice_{invoiceDate}_{order.OrderNumber}.html");
        string content = RenderHtmlInvoice(order, contractor);
        File.WriteAllText(fileName, content, Encoding.UTF8);
        return fileName;
    }

    private static string Escape(string value)
    {
        return System.Net.WebUtility.HtmlEncode(value ?? string.Empty);
    }
}
