using FCG.Shared.Domain.Exceptions;
using FCG.Users.Application.Abstractions.Messaging;
using FCG.Users.Application.DTOs;
using FCG.Shared.Contracts.Events;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Enums;
using FCG.Users.Domain.Repositories;
using FCG.Users.Domain.Security;
using FCG.Users.Domain.ValueObjects;

namespace FCG.Users.Application.UseCases;

public sealed class CriarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISenhaHasher _senhaHasher;
    private readonly IEventPublisher _transit;
    public CriarUsuarioUseCase
    (
        IUsuarioRepository usuarioRepository,
        ISenhaHasher senhaHasher,
        IEventPublisher transit
    )
    {
        _transit = transit;
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

        await _transit.PublishAsync(
            new UserCreatedEvent(
                UsuarioId: usuario.Id,
                Email: usuario.Email.Valor,
                Nome: usuario.Nome
            )
        );
        return usuario.Id;
    }
}
