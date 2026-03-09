using FCG.Users.Domain.Identidade.Repositories;
using Moq;

namespace FCG.Users.BDDTests.Identidade.Logout
{
	public sealed class LogoutContext
	{
		public Mock<IRefreshTokenRepository> RefreshTokenRepository { get; } = new();
		public Exception? Excecao { get; set; }
	}
}
