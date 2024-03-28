using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class Role
{
    public byte[] Id { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public byte[] CreatedBy { get; set; } = null!;

    public byte[] ModifiedBy { get; set; } = null!;


    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
