using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.Authentication;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
