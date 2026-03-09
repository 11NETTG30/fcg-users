using FCG.Users.Domain.ValueObjects;

namespace FCG.Users.Domain.Security;

public interface ISenhaHasher
{
    SenhaHash GerarHash(SenhaTextoPuro senhaTextoPuro);
    bool ValidarSenha(string senhaTextoPuro, SenhaHash senhaHash);
}
