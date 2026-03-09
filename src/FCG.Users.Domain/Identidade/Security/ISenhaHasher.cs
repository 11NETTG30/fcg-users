using FCG.Users.Domain.Identidade.ValueObjects;

namespace FCG.Users.Domain.Identidade.Security;

public interface ISenhaHasher
{
    SenhaHash GerarHash(SenhaTextoPuro senhaTextoPuro);
    bool ValidarSenha(string senhaTextoPuro, SenhaHash senhaHash);
}
