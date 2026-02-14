using ErrorOr;

namespace Bind.Web.Common.Http;

public static class ProblemDetailsExtensions
{
    public static IResult Problem(this IReadOnlyList<Error> errors)
    {
        if (errors.Count is 0)
            return Results.Problem();

        if (errors.All(error => error.Type is ErrorType.Validation))
            return ValidationProblem(errors);

        return Problem(errors[0]);
    }

    private static IResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(
            detail: error.Description,
            statusCode: statusCode,
            title: error.Code);
    }

    private static IResult ValidationProblem(IReadOnlyList<Error> errors)
    {
        var modelState = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray());

        return Results.ValidationProblem(modelState);
    }
}