using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Together.Domain.SeedWork
{
    public abstract class Enumeration
        : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected Enumeration() { }
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.DeclaredOnly);

            foreach(var filed in fields)
            {
                var instance = new T();
                var value = filed.GetValue(instance) as T;
                if (value != null)
                {
                    yield return value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Enumeration;
            if(other==null)
            {
                return false;
            }
            var typeMatches = GetType().Equals(obj.GetType());
            var valueMathes = Id.Equals(other.Id);

            return typeMatches
                && valueMathes;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration first, Enumeration second)
        {
            var absoluteDifferences = Math.Abs(first.Id - second.Id);
            return absoluteDifferences;
        }

        public static T FromValue<T>(int value)
            where T:Enumeration,new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) 
            where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
}
