namespace CfpService.Contracts.Errors;

public static class ApplicationErrors
{
    public static IError ApplicationNotFound(Guid id) => 
        new Error(400, $"Application with id {id} not found.");

    public static IError UserUnsubmittedApplicationNotFound() => 
        new Error(400, "User don't have any unsubmitted application");
    
    public static IError UserHasUnsubmittedApplication() => 
        new Error(400, "Cannot create new application, user has not-submitted application");

    public static IError CannotEditSubmittedApplication() =>
        new Error(400, "Cannot edit submitted application");

    public static IError UnspecifiedParameter() =>
        new Error(400, "specify exactly one of the following query parameters: 'submittedAfter' or 'unsubmittedOlder'");
    
    public static IError CannotSubmitInvalidApplication() =>
        new Error(400, "Cannot submit, missing key fields");
    
    public static IError AlreadySubmittedApplication() =>
        new Error(400, "Application was already submitted");
}