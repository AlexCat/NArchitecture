using System.Security.Claims;

namespace NArchitecture
{
    public interface IUserContext
    {
        ClaimsPrincipal Current { get; }
    }
}
