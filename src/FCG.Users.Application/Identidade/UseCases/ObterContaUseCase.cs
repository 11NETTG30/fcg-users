using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Application.Shared;
using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Identidade.Repositories;

namespace FCG.Users.Application.Identidade.UseCases;

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
