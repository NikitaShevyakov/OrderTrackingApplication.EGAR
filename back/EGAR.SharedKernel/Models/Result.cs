using EGAR.SharedKernel.Enums;

namespace EGAR.SharedKernel.Models;

public class Result
{
    public bool IsSuccess { get; }
    public ErrorType ErrorType { get; }
    public string ErrorMessage { get; }

    protected Result(bool isSuccess, ErrorType errorType, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }
    public static Result Success() =>
       new(true, ErrorType.None, string.Empty);

    public static Result Failure(ErrorType errorType, string message) =>
        new(false, errorType, message);
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(bool isSuccess, T value, ErrorType errorType, string errorMessage)
        : base(isSuccess, errorType, errorMessage)
    {
        Value = value;
    }
    public static Result<T> Success(T value) =>
        new(true, value, ErrorType.None, string.Empty);

    public static Result<T> Failure(ErrorType errorType, string message) =>
        new(false, default, errorType, message);
}
