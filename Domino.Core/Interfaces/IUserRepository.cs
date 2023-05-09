using Domino.Core.Entities;
using Domino.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Core.Interfaces
{
    public interface IUserRepository 
    {
        Task<User> GetLogin(User login);
    }
}
