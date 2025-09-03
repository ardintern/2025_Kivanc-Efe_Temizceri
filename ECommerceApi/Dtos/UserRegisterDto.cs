using Microsoft.Identity.Client;

namespace ECommerceApi.Dtos
{
    public class UserRegisterDto
    {
    
    
      public long Tc {  get; set; }
      public string? Email { get; set; }
      public string? Password { get; set; }
      public string? RePassword { get; set; }

    
    
    }
}
