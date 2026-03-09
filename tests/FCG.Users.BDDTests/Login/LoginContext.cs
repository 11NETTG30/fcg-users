using FCG.Users.Application.DTOs;
using FCG.Users.Application.Security;
using FCG.Users.Domain.Repositories;
using FCG.Users.Domain.Security;
using Moq;

namespace FCG.Users.BDDTests.Login
{
	public sealed class LoginContext
	{
		public Mock<IUsuarioRepository> UsuarioRepository { get; } = new();
		public Mock<IRefreshTokenRepository> RefreshTokenRepository { get; } = new();
		public Mock<IJwtService> JwtService { get; } = new();
		public Mock<ISenhaHasher> SenhaHasher { get; } = new();
		public Mock<ITokenSettings> TokenSettings { get; } = new();
		public AuthResponse? Response { get; set; }
		public Exception? Excecao { get; set; }
	}
}
