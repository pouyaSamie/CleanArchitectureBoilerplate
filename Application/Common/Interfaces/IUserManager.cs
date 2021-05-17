using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<Result<long>> CreateUserAsync(string userName, string password);

        Task<Result<string>> DeleteUserAsync(long userId);
    }
}
