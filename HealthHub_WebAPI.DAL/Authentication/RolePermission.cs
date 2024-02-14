using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.Authentication;

public partial class RolePermission
{
    public int RolePermissionId { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual Role? Role { get; set; }
}
