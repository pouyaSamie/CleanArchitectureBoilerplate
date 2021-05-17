using Microsoft.AspNetCore.Identity;
using Application.Common.Models;
using System.Linq;

namespace Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {

        public static Result<T> ToApplicationResult<T>(this IdentityResult result, T data)
        {
            return result.Succeeded
                ? Result<T>.Success(data)
                : Result<T>.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
