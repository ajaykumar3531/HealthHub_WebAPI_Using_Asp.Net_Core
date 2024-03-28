using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class User
{
    public byte[] Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public byte[] AddressId { get; set; } = null!;

    public byte[]? ReportTo { get; set; }

    public byte[]? ParentUserId { get; set; }

    public short Type { get; set; }

    public byte[]? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Dob { get; set; } = null!;

    public short Gender { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Specialty { get; set; }

    public byte[]? RoleId { get; set; }

    public short Status { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Appointment> AppointmentDoctorUsers { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentPatientUsers { get; set; } = new List<Appointment>();

    public virtual ICollection<BillingAndPayment> BillingAndPayments { get; set; } = new List<BillingAndPayment>();


    public virtual Role? Role { get; set; }
}
