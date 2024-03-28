using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class BillingAndPayment
{
    public byte[] BillId { get; set; } = null!;

    public byte[] PatientId { get; set; } = null!;

    public DateOnly? DateIssued { get; set; }

    public DateOnly? DueDate { get; set; }


    public decimal? TotalAmount { get; set; }

    public decimal? AmountPaid { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? InvoiceNumber { get; set; }

    public int? InsuranceClaimId { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public byte[]? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public byte[]? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public decimal? DiscountsOrAdjustments { get; set; }

    public decimal? Balance { get; set; }

    public byte[]? ProviderId { get; set; }

    public string? ServiceDescription { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentGatewayTransactionId { get; set; }

    public virtual User Patient { get; set; } = null!;
}
