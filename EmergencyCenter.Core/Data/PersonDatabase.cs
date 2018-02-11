using System;
using System.Collections;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts.Data;
using EmergencyCenter.Units.Contracts.Characters;

namespace EmergencyCenter.Core.Data
{
    public class PersonDatabase<TPerson> : IDatabase<TPerson> where TPerson : IPerson
    {
        private const string CannotAddNullMessage = "Cannot add null to person database.";
        private const string PersonAlreadyExistMessage = "Person with give id already exist in database.";
        private const string PredicatCannotBeNullMessage = "Predicat cannot be null.";

        private readonly IDictionary<int, TPerson> persons;

        public PersonDatabase()
        {
            this.persons = new Dictionary<int, TPerson>();
        }

        public void Add(TPerson person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(CannotAddNullMessage);
            }
            if (this.persons.ContainsKey(person.Id))
            {
                throw new ArgumentException(PersonAlreadyExistMessage);
            }

            this.persons.Add(person.Id, person);
        }

        public void Remove(TPerson person)
        {
            if (person == null)
            {
                return;
            }

            this.persons.Remove(person.Id);
        }

        public void RemoveByCriteria(Predicate<TPerson> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(PredicatCannotBeNullMessage);
            }

            var first = default(TPerson);
            var isMatch = false;

            foreach (var person in this.persons.Values)
            {
                if (match(person))
                {
                    first = person;
                    isMatch = true;
                    break;
                }
            }

            if (isMatch)
            {
                this.persons.Remove(first.Id);
            }
        }

        public void RemoveAllByCriteria(Predicate<TPerson> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(PredicatCannotBeNullMessage);
            }

            var matches = new List<TPerson>();
            var isMatch = false;

            foreach (var person in this.persons.Values)
            {
                if (match(person))
                {
                    matches.Add(person);
                    isMatch = true;
                    break;
                }
            }

            if (isMatch)
            {
                foreach (var personMatch in matches)
                {
                    this.persons.Remove(personMatch.Id);
                }
            }
        }

        public TPerson ReturnByCriteria(Predicate<TPerson> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(PredicatCannotBeNullMessage);
            }

            foreach (var person in this.persons.Values)
            {
                if (match(person))
                {
                    return person;
                }
            }

            return default(TPerson);
        }

        public IEnumerable<TPerson> ReturnAllByCriteria(Predicate<TPerson> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(PredicatCannotBeNullMessage);
            }

            var matches = new List<TPerson>();
            var isMatch = false;

            foreach (var person in this.persons.Values)
            {
                if (match(person))
                {
                    matches.Add(person);
                    isMatch = true;
                    break;
                }
            }

            if (isMatch)
            {
                return matches;
            }

            return null;
        }

        public IEnumerator<TPerson> GetEnumerator()
        {
            return this.persons.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
