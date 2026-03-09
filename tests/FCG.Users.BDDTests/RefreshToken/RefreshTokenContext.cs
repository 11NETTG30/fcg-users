using FCG.Users.Application.DTOs;
using FCG.Users.Application.Security;
using FCG.Users.Application.UseCases;
using FCG.Users.Domain.Repositories;
using FCG.Users.Domain.Services;
using Moq;

namespace FCG.Users.BDDTests.RefreshToken
{
	public sealed class RefreshTokenContext
	{
		public Mock<IUsuarioRepository> UsuarioRepository { get; } = new();
		public Mock<IRefreshTokenRepository> RefreshTokenRepository { get; } = new();
		public Mock<IJwtService> JwtService { get; } = new();
		public Mock<ITokenSettings> TokenSettings { get; } = new();
		public Mock<IRefreshTokenDomainService> RefreshTokenDomainService { get; } = new();
		public Mock<Microsoft.Extensions.Logging.ILogger<RefreshTokenUseCase>> Logger { get; } = new();
		public AuthResponse? Response { get; set; }
		public Exception? Excecao { get; set; }
		public Guid RefreshTokenId { get; set; }
	}
}
