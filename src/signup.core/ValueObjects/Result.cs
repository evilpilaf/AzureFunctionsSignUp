using System;

namespace SignUp.core.ValueObjects
{
    public class Result
    {
        public bool IsSuccess => Exception == null;
        public readonly Exception Exception;

        protected Result()
        {
            Exception = null;
        }

        public static Result Success() => new Result();

        protected Result(Exception exception)
        {
            Exception = exception;
        }

        public static implicit operator Result(Exception exception)
        {
            return new Result(exception);
        }
    }

    public sealed class Result<T> : Result
    {
        public T Content { get; }

        private Result(T content)
        {
            Content = content;
        }

        private Result(Exception exception)
            : base(exception)
        {
        }

        public static implicit operator T(Result<T> instance)
        {
            return instance.Content;
        }

        public static implicit operator Result<T>(T instance)
        {
            return new Result<T>(instance);
        }

        public static implicit operator Result<T>(Exception exception)
        {
            return new Result<T>(exception);
        }

        public static Result<T> Success(T content) => new Result<T>(content);
    }
}
