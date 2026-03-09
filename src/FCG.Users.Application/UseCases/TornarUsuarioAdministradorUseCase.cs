using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Enums;
using FCG.Users.Domain.Repositories;
using FCG.Shared.Domain.Exceptions;

namespace FCG.Users.Application.UseCases;

public sealed class TornarUsuarioAdministradorUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public TornarUsuarioAdministradorUseCase
    (
        IUsuarioRepository usuarioRepository
    )
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task Executar(Guid id)
    {
        Usuario usuario = await _usuarioRepository.ObterPorIdTracking(id)
            ?? throw new ValidationException("Usuário não encontrado");

        usuario.SetPerfil(PerfilUsuario.Administrador);

        await _usuarioRepository.UnitOfWork.Commit();
    }
}
