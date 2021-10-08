using System.ComponentModel;

namespace Kookboek.Models
{
    public class UserModel
    {
        [DisplayName("Username")]
        public string Username { get; set; }
        
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}