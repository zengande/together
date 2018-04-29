using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Domain.SeedWork
{
    public interface IRepository<T> 
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
