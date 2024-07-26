using System;

namespace PKMGerejaEbenhaezer.Domain.Shared
{
    public class Result<T>
    {
        private readonly T _value;

        public T Value { 
            get 
            {
                if (IsFailure) throw new InvalidOperationException("Try to access Value of Failure Result Object");

                return _value;
            } 
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error[] Errors { get; }

        public Result(T value, bool isSuccess, params Error[] errors)
        {
            _value = value;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static implicit operator Result<T>(T value) => Result.Success(value);
    }

    public class Result
    {
        public static Result<T> Success<T>(T value) => new(value, true);

        public static Result<T> Failure<T>(Error error) => new(default, false, error);

        public static Result<T> Failure<T>(params Error[] errors) => new(default, false, errors);
    }
}
