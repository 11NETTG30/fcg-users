using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Application.Identidade.Security;
using FCG.Users.Domain.Identidade.Repositories;
using FCG.Users.Domain.Identidade.Security;
using Moq;

namespace FCG.Users.BDDTests.Identidade.Login
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
