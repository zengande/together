using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T entity);
    }
}
