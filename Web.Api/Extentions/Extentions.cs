using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace Web.Api.Extentions
{
    public static class Extentions
    {
        public static ActionResult<T> ToActionResultOrNotFound<T>(this Result<T> result) =>
            result.IsSuccess ?
            (ActionResult)new OkObjectResult(result.Data) :
            new NotFoundObjectResult(result.Errors);

        public static IActionResult ResultOrNotFound<T>(this Result<T> result) =>
           result.IsSuccess ?
           (ActionResult)new OkObjectResult(result.Data) :
           new NotFoundObjectResult(result.Errors);

        public static IActionResult ToActionResultOrBadRequest<T>(this Result<T> result) =>
                result.IsSuccess ?
                (IActionResult)new OkObjectResult(result.Data) :
                new BadRequestObjectResult(result.Errors);

        public static IActionResult SuccessMsgOrBadRequest<T>(this Result<T> result) =>
               result.IsSuccess ?
               (IActionResult)new OkObjectResult(result.SuccessMessages) :
               new BadRequestObjectResult(result.Errors);
    }
}
