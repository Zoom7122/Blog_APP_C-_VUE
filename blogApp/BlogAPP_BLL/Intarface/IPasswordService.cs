using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string storedHash);
    }
}
