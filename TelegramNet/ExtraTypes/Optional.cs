using System;

namespace TelegramNet.ExtraTypes
{
    public struct Optional<T>
    {
	    private readonly T _value;

	    public Optional(T value)
	    {
		    if (value != null)
		    {
			    _value = value;
			    HasValue = true;
		    }
		    else
		    {
			    _value = default;
			    HasValue = false;
		    }
	    }

	    public bool HasValue { get; }

	    public T Value => HasValue ? _value : throw new InvalidOperationException();

	    public override int GetHashCode()
        {
            return HashCode.Combine(_value, HasValue);
        }

        public override string ToString()
        {
            return HasValue ? _value.ToString() : "NoVal";
        }

        public T GetValueForce()
        {
            return !HasValue ? default : _value;
        }

        public override bool Equals(object obj)
        {
	        return obj is Optional<T> optional && Equals(optional);
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
            {
	            return Equals(_value, other._value);
            }

            return HasValue == other.HasValue;
        }

        public static explicit operator T(Optional<T> optional)
        {
	        return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
	        return value == null ? new Optional<T>(default) : new Optional<T>(value);
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