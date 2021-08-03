using System;

namespace TelegramNet.ExtraTypes
{
    public struct Optional<T>
    {
        public override int GetHashCode()
        {
            return HashCode.Combine(value, HasValue);
        }

        public bool HasValue { get; }
        private T value;

        public T Value
        {
            get
            {
                if (HasValue)
                    return value;
                else
                    throw new InvalidOperationException();
            }
        }

        public Optional(T value)
        {
            if (value != null)
            {
                this.value = value;
                HasValue = true;
            }
            else
            {
                this.value = value;
                HasValue = false;
            }
        }

        public T GetValueForce()
        {
            return !HasValue ? default : value;
        }

        public static explicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
            if (value != null)
                return new Optional<T>(value);

            return new Optional<T>(default);
        }

        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
                return Equals((Optional<T>) obj);

            return false;
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
                return Equals(value, other.value);

            return HasValue == other.HasValue;
        }

        public static bool operator ==(Optional<T> left, Optional<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Optional<T> left, Optional<T> right)
        {
            return !(left == right);
        }
    }
}