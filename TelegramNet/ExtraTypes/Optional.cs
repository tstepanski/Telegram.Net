using System;

namespace TelegramNet.ExtraTypes
{
    public struct Optional<T>
    {
        public override int GetHashCode()
        {
            return HashCode.Combine(value, HasValue);
        }

        public bool HasValue { get; private set; }
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
            this.value = value;
            HasValue = true;
        }

        public static explicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
            return new(value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
                return Equals((Optional<T>) obj);
            else
                return false;
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
                return Equals(value, other.value);
            else
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