using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class AppUserRole : IdentityUserRole<int>
{   
    public required AppUser User { get; set; }
    public required AppRole Role { get; set; }
}
