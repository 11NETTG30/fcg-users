using FCG.Users.Application.DTOs;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Repositories;

namespace FCG.Users.Application.UseCases;

public sealed class ListarTodosUsuariosUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ListarTodosUsuariosUseCase
    (
        IUsuarioRepository usuarioRepository
    )
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> Executar()
    {
        List<Usuario> usuarios = await _usuarioRepository.ListarTodos();

        return usuarios.Select(usuario => (UsuarioDto)usuario);
    }
}
