using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Application.Identidade.UseCases;
using FCG.Users.BDDTests.Support;
using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Identidade.Repositories;
using FCG.Users.Domain.Identidade.Security;
using FCG.Users.Domain.Identidade.ValueObjects;
using FCG.Shared.Domain.Exceptions;
using Moq;

namespace FCG.Users.BDDTests.Identidade.CriarUsuario;

[Binding]
public sealed class CriarUsuarioSteps
{
	private readonly CriarUsuarioContext _context;
	private readonly Mock<IUsuarioRepository> _usuarioRepository;
	private readonly Mock<ISenhaHasher> _senhaHasher;
	private readonly CriarUsuarioUseCase _useCase;

	public CriarUsuarioSteps(CriarUsuarioContext context)
	{
		_context = context;

		_usuarioRepository = new Mock<IUsuarioRepository>();
		_senhaHasher = new Mock<ISenhaHasher>();

		_senhaHasher
			.Setup(x => x.GerarHash(It.IsAny<SenhaTextoPuro>()))
			.Returns(new SenhaHash(
				CriarSenhaHashValidaFake()
			));

		_usuarioRepository
			.SetupGet(x => x.UnitOfWork)
			.Returns(new FakeUnitOfWork());

		_usuarioRepository
			.Setup(x => x.Adicionar(It.IsAny<Usuario>()))
			.Returns(Task.CompletedTask)
			.Callback<Usuario>(u => _context.UsuarioPersistido = u);

		_useCase = new CriarUsuarioUseCase(
			_usuarioRepository.Object,
			_senhaHasher.Object
		);
	}

	[Given(@"que não existe usuário cadastrado com o e-mail ""(.*)""")]
	public void DadoQueNaoExisteUsuarioCadastradoComEmail(string email)
	{
		_usuarioRepository
			.Setup(x => x.VerificarExistenciaEmail(email))
			.ReturnsAsync(false);
	}

	[Given(@"que já existe usuário cadastrado com o e-mail ""(.*)""")]
	public void DadoQueJaExisteUsuarioCadastradoComEmail(string email)
	{
		_usuarioRepository
			.Setup(x => x.VerificarExistenciaEmail(email))
			.ReturnsAsync(true);
	}

	[When(@"o usuário é criado com nome ""(.*)"", e-mail ""(.*)"" e senha ""(.*)""")]
	public async Task QuandoUsuarioEhCriado(string nome, string email, string senha)
	{
		_context.Request = new CriarUsuarioRequest(
			nome,
			email,
			senha,
			senha
		);

		try
		{
			_context.UsuarioId = await _useCase.Executar(_context.Request);
		}
		catch (Exception ex)
		{
			_context.Excecao = ex;
		}
	}

	[Then(@"o usuário deve ser persistido")]
	public void EntaoUsuarioDeveSerPersistido()
	{
		_context.Excecao.Should().BeNull();
		_context.UsuarioPersistido.Should().NotBeNull();
	}

	[Then(@"deve ser retornado um identificador válido")]
	public void EntaoDeveSerRetornadoIdentificadorValido()
	{
		_context.Excecao.Should().BeNull();
		_context.UsuarioId.Should().NotBeEmpty();
	}

	[Then(@"deve ocorrer um erro de conflito")]
	public void EntaoDeveOcorrerErroDeConflito()
	{
		_context.Excecao.Should().BeOfType<ConflictException>();
	}
	private static string CriarSenhaHashValidaFake()
	{
		return new string('A', SenhaHash.TAMANHO_ESPERADO_SENHA_HASH);
	}
}
