using System;

namespace EFCore6CosmosValueConverter
{
    public class TypedId<TKey> : IEquatable<TypedId<TKey>>
    {
        public TKey Value { get; }

        public TypedId(TKey value)
        {
            if (value.Equals(default(TKey)))
            {
                throw new Exception("Id value cannot be empty!");
            }
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            return obj is TypedId<TKey> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(TypedId<TKey> other)
        {
            return this.Value.Equals(other.Value);
        }

        public static bool operator ==(TypedId<TKey> obj1, TypedId<TKey> obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypedId<TKey> x, TypedId<TKey> y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
