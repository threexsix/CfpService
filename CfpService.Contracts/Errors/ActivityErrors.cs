namespace CfpService.Contracts.Errors;

public static class ActivityErrors
{
    public static IError ActivitiesNotFound() => 
        new Error(400, $"Activities were not found.");
}