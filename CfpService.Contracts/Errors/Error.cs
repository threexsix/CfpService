namespace CfpService.Contracts.Errors;

public class Error : IError
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public Error(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}