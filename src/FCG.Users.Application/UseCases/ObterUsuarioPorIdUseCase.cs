using FCG.Users.Application.DTOs;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Repositories;

namespace FCG.Users.Application.UseCases;

public sealed class ObterUsuarioPorIdUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterUsuarioPorIdUseCase
    (
        IUsuarioRepository usuarioRepository
    )
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto?> Executar(Guid id)
    {
        Usuario? usuario = await _usuarioRepository.ObterPorId(id);

        if (usuario is null)
            return null;

        return (UsuarioDto)usuario;
    }
}
