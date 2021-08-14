using System;

namespace TelegramNet.ExtraTypes
{
    public struct Optional<T>
    {
	    private readonly T _value;
	    
        public override int GetHashCode()
        {
            return HashCode.Combine(_value, HasValue);
        }

        public override string ToString()
        {
            return $"{(HasValue ? _value.ToString() : "NoVal")}";
        }

        public bool HasValue { get; }

        public T Value
        {
            get
            {
                if (HasValue)
                {
	                return _value;
                }
                else
                {
	                throw new InvalidOperationException();
                }
            }
        }

        public Optional(T value)
        {
            if (value != null)
            {
                this._value = value;
                HasValue = true;
            }
            else
            {
                this._value = value;
                HasValue = false;
            }
        }

        public T GetValueForce()
        {
            return !HasValue ? default : _value;
        }

        public static explicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
            if (value != null)
            {
	            return new Optional<T>(value);
            }

            return new Optional<T>(default);
        }

        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
            {
	            return Equals((Optional<T>) obj);
            }

            return false;
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
            {
	            return Equals(_value, other._value);
            }

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