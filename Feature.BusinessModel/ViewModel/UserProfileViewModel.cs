using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.BusinessModel.ViewModel
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = "1234";
      
    }
}
