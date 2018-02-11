using System;
using System.Collections.Generic;

namespace EmergencyCenter.Core.Contracts.Data
{
    public interface IDatabase<T> : IEnumerable<T>
    {
        void Add(T element);

        void Remove(T element);

        void RemoveByCriteria(Predicate<T> match);

        void RemoveAllByCriteria(Predicate<T> match);

        T ReturnByCriteria(Predicate<T> match);

        IEnumerable<T> ReturnAllByCriteria(Predicate<T> match);
    }
}
