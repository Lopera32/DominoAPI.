using Domino.Core.Entities;
using Domino.Core.Interfaces;
using Domino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DominoContext _context;

        public UserRepository(DominoContext context)
        {
            _context = context;
        }

        public async Task<User> GetLogin(User login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email== login.Email);
        }
    }
}
