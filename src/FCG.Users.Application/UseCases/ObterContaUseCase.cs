using FCG.Users.Application.DTOs;
using FCG.Shared.Application;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Repositories;

namespace FCG.Users.Application.UseCases;

public sealed class ObterContaUseCase
{
    private readonly IInformacoesUsuarioLogado _informacoesUsuarioLogado;
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterContaUseCase
    (
        IInformacoesUsuarioLogado informacoesUsuarioLogado,
        IUsuarioRepository usuarioRepository
    )
    {
        _informacoesUsuarioLogado = informacoesUsuarioLogado;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto?> Executar()
    {
        Usuario? usuario = await _usuarioRepository.ObterPorId(_informacoesUsuarioLogado.Id);

        if (usuario is null)
            return null;

        return (UsuarioDto)usuario;
    }
}
