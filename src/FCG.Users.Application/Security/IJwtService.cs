using FCG.Users.Domain.Entities;

namespace FCG.Users.Application.Security;

public interface IJwtService
{
    string GerarAccessToken(Usuario usuario);
}
