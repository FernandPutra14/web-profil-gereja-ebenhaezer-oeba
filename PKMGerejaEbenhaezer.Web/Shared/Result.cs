using System;
using System.Linq;

namespace PKMGerejaEbenhaezer.Domain.Shared
{
    public class Result<T>
    {
        private readonly T? _value;

        public T Value => IsSuccess 
            ? _value! : throw new InvalidOperationException("Try to access Value of failure Result");

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error[] Errors { get; }
        public Error Error => Errors.FirstOrDefault() ?? Error.None;

        private Result(T? value, bool isSuccess, params Error[] errors)
        {
            if(isSuccess && value is null)
                throw new ArgumentException("Null value of Success Result", nameof(value));

            if (!isSuccess && errors.Length == 0)
                throw new ArgumentException("Failure Result must have at least 1 error", nameof(errors));

            _value = value;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result<T> Success(T value) => new(value, true);

        public static Result<T> Failure(params Error[] errors) => new(default, false, errors);

        public static implicit operator Result<T>(T value) => Result<T>.Success(value);

        public static implicit operator Result<T>(Error error) => Result<T>.Failure(error);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error[] Errors { get; }
        public Error Error => Errors.FirstOrDefault() ?? Error.None;

        private Result(bool isSuccess, params Error[] errors)
        {
            if (isSuccess && errors.Length > 0)
                throw new ArgumentException("errors on Success Result", nameof(errors));

            if (!isSuccess && errors.Length == 0)
                throw new ArgumentException("Empty errors on Failure Result", nameof(errors));

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true);

        public static Result Failure(params Error[] errors) => new(false, errors);

        public static implicit operator Result(Error error) => Failure(error);
    }
}
