using Microsoft.AspNetCore.Identity;
using PickABookWeb.Migrations;

namespace PickABookWeb.Models
{
    public class DefaultUser : IdentityUser 
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
