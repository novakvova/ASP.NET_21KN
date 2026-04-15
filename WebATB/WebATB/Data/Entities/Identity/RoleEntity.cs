using Microsoft.AspNetCore.Identity;

namespace WebATB.Data.Entities.Identity;

public class RoleEntity : IdentityRole<int>
{
    public ICollection<UserRoleEntity>? UserRoles { get; set; }
}
