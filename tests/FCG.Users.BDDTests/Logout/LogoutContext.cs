using FCG.Users.Domain.Repositories;
using Moq;

namespace FCG.Users.BDDTests.Logout
{
	public sealed class LogoutContext
	{
		public Mock<IRefreshTokenRepository> RefreshTokenRepository { get; } = new();
		public Exception? Excecao { get; set; }
	}
}
