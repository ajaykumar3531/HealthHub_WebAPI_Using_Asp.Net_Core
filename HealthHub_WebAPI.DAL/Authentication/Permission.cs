using System;
using System.Collections.Generic;

namespace HealthHub_WebAPI.DAL.Authentication;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
