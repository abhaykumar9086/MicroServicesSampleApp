using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global
{
    public interface IGateway
    {
        bool ValidateUser(Userobj user, string url);
    }

    public class Userobj
    {
        public int UserID { get; set; }
        public string password { get; set; }
    }
}
