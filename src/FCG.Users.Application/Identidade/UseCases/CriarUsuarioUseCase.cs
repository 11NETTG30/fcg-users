using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Identidade.Enums;
using FCG.Users.Domain.Identidade.Repositories;
using FCG.Users.Domain.Identidade.Security;
using FCG.Users.Domain.Identidade.ValueObjects;
using FCG.Shared.Domain.Exceptions;

namespace FCG.Users.Application.Identidade.UseCases;

public sealed class CriarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISenhaHasher _senhaHasher;

    public CriarUsuarioUseCase
    (
        IUsuarioRepository usuarioRepository,
        ISenhaHasher senhaHasher
    )
    {
        _usuarioRepository = usuarioRepository;
        _senhaHasher = senhaHasher;
    }

    public async Task<Guid> Executar(CriarUsuarioRequest request)
    {
        Email email = new(request.Email);

        SenhaTextoPuro senhaTextoPuro = new(request.Senha, request.ConfirmacaoSenha);
        SenhaHash senhaHash = _senhaHasher.GerarHash(senhaTextoPuro);

        Usuario usuario = new(request.Nome, email, senhaHash, PerfilUsuario.Usuario);

        bool emailExiste = await _usuarioRepository.VerificarExistenciaEmail(usuario.Email.Valor);

        if (emailExiste)
            throw new ConflictException("Já existe um usuário cadastrado com esse e-mail");

        await _usuarioRepository.Adicionar(usuario);
        await _usuarioRepository.UnitOfWork.Commit();

        return usuario.Id;
    }
}
