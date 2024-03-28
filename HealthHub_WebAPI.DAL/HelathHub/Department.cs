using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class Department
{
    public byte[] Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte[] CreatedBy { get; set; } = null!;


    public DateTime CreatedDate { get; set; }
}
