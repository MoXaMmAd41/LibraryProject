using Library.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library.Models
{
    public interface IAccountRepository
    {
        Task AddUser(User user, string password);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckPassword(User user, string enteredPassword);
        Task<bool> SignInUser(User user, string password);
        Task<IdentityResult> CreateUserAsync(User user, string password);
    }
}