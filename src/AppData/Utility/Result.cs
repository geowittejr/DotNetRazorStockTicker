
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppDataModels.Utility
{
    public class Result<T>
    {
        public bool IsSuccess => Error == null;
        public T? Value { get; }
        public ResultError? Error { get; } = null;

        // Static initialize methods
        public static Result<T> Success(T value) => new(value, null);
        public static Result<T> Failure(ResultError error) => new(default(T), error);

        private Result(T? value, ResultError? error)
        {
            Value = value;
            Error = error;
        }
    }
}
