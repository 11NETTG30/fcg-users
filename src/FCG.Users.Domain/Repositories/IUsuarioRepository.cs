using FCG.Users.Domain.Entities;
using FCG.Shared.Domain.Abstractions;

namespace FCG.Users.Domain.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<List<Usuario>> ListarTodos();
    Task<Usuario?> ObterPorId(Guid id);
    Task<Usuario?> ObterPorIdTracking(Guid id);
    Task<Usuario?> ObterPorEmail(string email);
    Task Adicionar(Usuario usuario);
    Task<bool> VerificarExistenciaEmail(string email);
}
