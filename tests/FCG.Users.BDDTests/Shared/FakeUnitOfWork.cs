using FCG.Users.Domain.Shared.UoW;

namespace FCG.Users.BDDTests.Support
{
	public sealed class FakeUnitOfWork : IUnitOfWork
	{
		public Task<bool> Commit() => Task.FromResult(true);
	}
}
