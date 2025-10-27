
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppDataModels.Utility
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ResultError? Error { get; }
        public static Result<T> Success(T value) => new(value, true, null);
        public static Result<T> Failure(ResultError error) => new(default(T), false, error);
        private Result(T? value, bool isSuccess, ResultError? error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
