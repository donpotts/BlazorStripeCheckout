using System.ComponentModel.DataAnnotations;

namespace BlazorStripeCheckout.Shared.Models;

public class ApplicationUserWithRolesDto : ApplicationUserDto
{
    public List<string>? Roles { get; set; }
}
