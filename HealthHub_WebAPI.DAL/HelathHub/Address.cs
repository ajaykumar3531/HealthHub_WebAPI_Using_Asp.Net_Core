using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class Address
{
    public byte[] Id { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string StatePinCode { get; set; } = null!;

    public string LandMark { get; set; } = null!;


    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
