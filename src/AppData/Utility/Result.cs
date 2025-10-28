
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppDataModels.Utility
{
    /// <summary>
    /// Represents the result of an operation that can either succeed with a value of type <typeparamref name="T"/>
    /// or fail with a <see cref="ResultError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned on success.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Gets the value returned when the operation succeeded.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Gets the error details when the operation failed.
        /// This property is <c>null</c> when the result represents a success.
        /// </summary>
        public ResultError? Error { get; } = null;

        /// <summary>
        /// Creates a successful <see cref="Result{T}"/> containing the supplied value.
        /// </summary>
        /// <param name="value">The successful value to store in the result.</param>
        /// <returns>A <see cref="Result{T}"/> representing success.</returns>
        public static Result<T> Success(T value) => new(value, null);

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> containing the supplied error.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        /// <returns>A <see cref="Result{T}"/> representing failure.</returns>
        public static Result<T> Failure(ResultError error) => new(default(T), error);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// This constructor is private; use <see cref="Success(T)"/> or <see cref="Failure(ResultError)"/> to create instances.
        /// </summary>
        /// <param name="value">The value when successful, or <c>default(T)</c> on failure.</param>
        /// <param name="error">The error when failed, or <c>null</c> on success.</param>
        private Result(T? value, ResultError? error)
        {
            Value = value;
            Error = error;
        }
    }
}
