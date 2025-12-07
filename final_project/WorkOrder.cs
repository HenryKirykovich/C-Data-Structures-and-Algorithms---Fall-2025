using System;

namespace FinalProject.WorkOrders;

public enum PaymentMethod
{
    Unknown = 0,
    Paypal,
    Zelle,
    BankAccount,
    Cash
}

public class WorkOrder
{
    public int OrderNumber { get; set; }
    public DateTime WorkDate { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal MaterialsAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Description { get; set; } = string.Empty;
    public double HoursWorked { get; set; }
    public bool IsSalesTaxIncluded { get; set; }
    public bool IsPaid { get; set; }
}
