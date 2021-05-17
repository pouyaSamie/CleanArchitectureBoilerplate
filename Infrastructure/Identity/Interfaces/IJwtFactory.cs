using Application.Common.Models;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IJwtFactory
    {
        Task<Result<ITokenModel>> GetTokenAsync(string userName, string Password);
    }
}
