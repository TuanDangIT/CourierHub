using CourierHub.Core.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Result;

/// <summary>
/// Represents the result of an operation, indicating success or failure, and optionally containing an error.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified success status and error.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result, if any.</param>
    /// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="errors"/> is invalid.</exception>
    protected Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        if (isSuccess && errors != null && errors.Count > 0 ||
            !isSuccess && (errors == null || errors.Count == 0))
        {
            throw new ArgumentException("Invalid combination of isSuccess and errors.", nameof(errors));
        }
        IsSuccess = isSuccess;
        Errors = errors ?? [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified success status.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    protected Result(bool isSuccess) => IsSuccess = isSuccess;

    /// <summary>
    /// Property indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Property indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Property representing the error associated with the result, if any.
    /// </summary>
    public IReadOnlyList<Error> Errors { get; } = [];

    /// <summary>
    /// Creates a successful result without any associated error.
    /// </summary>
    /// <returns>A <see cref="Result"/> representing a successful operation.</returns>
    public static Result Success() => new(true);

    /// <summary>
    /// Creates a successful result with an associated value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value associated with the result.</typeparam>
    /// <param name="value">The value to associate with the successful result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a successful operation with the specified value.</returns>
    public static Result<T> Success<T>(T value) => new(value, true);

    /// <summary>
    /// Creates a failed result with an associated error.
    /// </summary>
    /// <param name="errors">The errors to associate with the failed result.</param>
    /// <returns>A <see cref="Result"/> representing a failed operation with the specified errors.</returns>
    public static Result Failure(IReadOnlyList<Error> errors) => new(false, errors);

    /// <summary>
    /// Creates a failed result with an associated error and a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value associated with the result.</typeparam>
    /// <param name="errors">The errors to associate with the failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a failed operation with the specified errors.</returns>
    public static Result<T> Failure<T>(IReadOnlyList<Error> errors) => new(default!, false, errors);
}

/// <summary>
/// Represents the result of an operation that can either be successful with a value of type <typeparamref name="T"/> or failed with an associated error.
/// </summary>
/// <typeparam name="T">The type that is returned in result object.</typeparam>
public class Result<T> : Result
{

    /// <summary>
    /// The value associated with the result. This property is only valid when the result is successful.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value, success status, and error.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="success">Indicates whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the result, if any.</param>
    public Result(T value, bool success, IReadOnlyList<Error> errors) : base(success, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value success status.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="success">Indicates whether the operation was successful.</param>
    public Result(T value, bool success) : base(success)
    {
        Value = value;
    }
}
