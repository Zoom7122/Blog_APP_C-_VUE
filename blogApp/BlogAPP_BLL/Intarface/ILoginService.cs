using BlogAPP_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Intarface
{
    public interface ILoginService
    {
        Task<UserEnrance> Login(LoginDate data);

        Task<bool> Register(CreateUserDto data);
    }
}
