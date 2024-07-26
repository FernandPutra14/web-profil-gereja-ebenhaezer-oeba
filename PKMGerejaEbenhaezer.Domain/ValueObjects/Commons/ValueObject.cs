using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.ValueObjects.Commons
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object? obj)
        {
            return obj is not null && obj is ValueObject other && ValueObjectEquals(other);
        }

        public bool Equals(ValueObject? other)
        {
            return other is not null && ValueObjectEquals(other);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Aggregate(
                    default(int),
                    HashCode.Combine);
        }

        private bool ValueObjectEquals(ValueObject other)
        {
            return other.GetAtomicValues().SequenceEqual(GetAtomicValues());
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return left is not null && left.Equals(right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
}
