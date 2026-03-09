using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Shared.Abstractions;

namespace FCG.Users.Domain.Identidade.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<List<Usuario>> ListarTodos();
    Task<Usuario?> ObterPorId(Guid id);
    Task<Usuario?> ObterPorIdTracking(Guid id);
    Task<Usuario?> ObterPorEmail(string email);
    Task Adicionar(Usuario usuario);
    Task<bool> VerificarExistenciaEmail(string email);
}
