using FCG.Users.Domain.Identidade.Entities;

namespace FCG.Users.Application.Identidade.Security;

public interface IJwtService
{
    string GerarAccessToken(Usuario usuario);
}
