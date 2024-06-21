namespace SweatSheet.Server.Modules.Auth;

public static class AuthEndpoints
{
    public static void RegisterAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/api/v1/auth").WithTags("Auth");

        authGroup.MapGet("/user", AuthService.GetUser).RequireAuthorization();

        authGroup.MapPost("/login", AuthService.SignIn);

        authGroup.MapPost("/register", AuthService.Register);

        authGroup.MapPost("/logout", AuthService.SignOut);

        authGroup.MapPost("/changePassword", AuthService.ChangePassword);

        authGroup.MapGet("/adminRoute", () => Results.Ok("Admin route")).RequireAuthorization("admin");
    }
}
