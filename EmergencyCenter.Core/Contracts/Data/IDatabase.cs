using System;
using System.Collections.Generic;

namespace EmergencyCenter.Core.Contracts.Data
{
    public interface IDatabase<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T element);

        void Remove(T element);

        void RemoveByCriteria(Predicate<T> match);

        void RemoveAllByCriteria(Predicate<T> match);

        T Find(Predicate<T> match);

        IEnumerable<T> FindAll(Predicate<T> match);
    }
}
