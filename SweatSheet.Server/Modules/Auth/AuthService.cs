using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SweatSheet.Server.Modules.Auth.DTOs;
using SweatSheet.Server.Modules.Auth.Entities;

namespace SweatSheet.Server.Modules.Auth;

public static class AuthService
{
    public static async Task<IResult> SignIn(LoginDto dto, SignInManager<ApplicationUser> signInManager)
    {
        var result = await signInManager.PasswordSignInAsync(dto.Username, dto.Password, true, false);

        return result.Succeeded ? Results.Ok() : Results.BadRequest("Invalid credentials");
    }

    public static async Task<IResult> SignOut(SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    public static async Task<IResult> Register(
        RegisterDto dto,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
    )
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            return Results.BadRequest("Passwords do not match");
        }

        var user = new ApplicationUser() { UserName = dto.Username };

        var createUserResult = await userManager.CreateAsync(user, dto.Password);

        if (!createUserResult.Succeeded)
        {
            return Results.BadRequest(createUserResult.Errors);
        }

        await userManager.AddToRoleAsync(user, "user");

        await signInManager.SignInAsync(user, true);

        return Results.Ok();
    }

    public static async Task<IResult> GetUser(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager
    )
    {
        var currentUser = await userManager.GetUserAsync(principal);

        if (currentUser == null)
        {
            return Results.Unauthorized();
        }

        var userRoles = await userManager.GetRolesAsync(currentUser);

        return Results.Ok(
            new Dictionary<string, string>
            {
                { "userName", currentUser.UserName ?? "" },
                { "userId", currentUser.Id },
                { "role", userRoles[0] }
            }
        );
    }

    public static async Task<IResult> ChangePassword(
        ChangePasswordDto dto,
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager
    )
    {
        var currentUser = await userManager.GetUserAsync(principal);

        if (currentUser == null)
        {
            return Results.Unauthorized();
        }

        var newPasswordMatch = dto.NewPassword == dto.ConfirmPassword;

        if (!newPasswordMatch)
        {
            return Results.BadRequest("Passwords do not match");
        }

        var result = await userManager.ChangePasswordAsync(currentUser, dto.OldPassword, dto.NewPassword);

        return result.Succeeded ? Results.Ok() : Results.BadRequest(result.Errors);
    }
}
