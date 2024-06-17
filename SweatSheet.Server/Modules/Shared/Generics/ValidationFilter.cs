using FluentValidation;

namespace SweatSheet.Server;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator == null)
        {
            return await next(context);
        }

        var entity = ValidationFilter<T>.GetEntityToValidate(context);

        if (entity == null)
        {
            return Results.Problem("Could not find type to validate");
        }

        var validationResult = await validator.ValidateAsync(entity);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);
    }

    private static T? GetEntityToValidate(EndpointFilterInvocationContext context)
    {
        return context.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T));
    }
}
