using BlogAPP_Core.Models;
using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface ILoginService
    {
        Task<UserEnrance> Login(LoginDate data);

        Task<bool> Register(CreateUserDto data);

        Task<User> FindUserByEmail(string email);
    }
}
