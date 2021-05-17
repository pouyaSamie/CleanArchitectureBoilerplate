using System;

namespace Infrastructure.Identity.Interfaces
{
    public interface ITokenModel
    {
        string Token { get; set; }
        DateTime Expiration { get; set; }
    }
}
