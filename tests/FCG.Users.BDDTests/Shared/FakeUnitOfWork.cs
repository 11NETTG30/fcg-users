using FCG.Shared.Domain.UoW;

namespace FCG.Users.BDDTests.Support
{
	public sealed class FakeUnitOfWork : IUnitOfWork
	{
		public Task<bool> Commit() => Task.FromResult(true);
	}
}
