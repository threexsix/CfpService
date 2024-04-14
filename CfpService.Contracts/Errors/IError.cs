namespace CfpService.Contracts.Errors;

public interface IError
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }
}