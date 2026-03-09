using FCG.Users.Domain.Shared.UoW;

namespace FCG.Users.Domain.Shared.Abstractions;

public interface IRepository<T> : IDisposable where T : Entity, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
