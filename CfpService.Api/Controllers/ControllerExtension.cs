using CfpService.Contracts.Results;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

public static class ControllerExtensions
{
    public static ActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.Success)
            return controller.Ok(result.Value);

        return controller.StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });
    }

    public static IActionResult HandleResult(this ControllerBase controller, Result result)
    {
        if (result.Success)
            return controller.Ok();

        return controller.StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });
    }
}