using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(UserJwtDto user)

          where TAccessToken : IAccessToken, new();

        string GenerateRefreshToken();
    }
}