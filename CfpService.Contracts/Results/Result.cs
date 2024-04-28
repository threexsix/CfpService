using CfpService.Contracts.Errors;

namespace CfpService.Contracts.Results;
public class Result
{
    public bool Success { get; }
    public IError Error { get; }
    public bool Failure => !Success;

    protected Result(bool success, IError error)
    {
        Success = success;
        Error = error;
    }

    public static Result Fail(IError error)
    {
        return new Result(false, error);
    }

    public static Result<T> Fail<T>(IError error)
    {
        return new Result<T>(default(T), false, error);
    }

    public static Result Ok()
    {
        return new Result(true, null);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, null);
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool success, IError error) : base(success, error)
    {
        Value = value;
    }
}
