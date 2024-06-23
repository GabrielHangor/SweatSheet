using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SweatSheet.Server;
using SweatSheet.Server.Modules.Auth;
using SweatSheet.Server.Modules.Auth.Entities;
using SweatSheet.Server.Modules.Exercise;
using SweatSheet.Server.Modules.Exercise.DTOs;
using SweatSheet.Server.Modules.Exercise.EndpointValidators;
using SweatSheet.Server.Modules.Workouts;
using SweatSheet.Server.Modules.Workouts.DTOs;
using SweatSheet.Server.Modules.Workouts.EndpointValidators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlite("Data Source=workouts.db");
    },
    ServiceLifetime.Singleton
);

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("default", pb => pb.RequireRole("user", "admin"))
    .AddPolicy("admin", pb => pb.RequireRole("admin"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "SweatSheet.Identity";
    options.Cookie.SameSite = SameSiteMode.Strict;

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddTransient<WorkoutsService>();
builder.Services.AddTransient<ExercisesService>();

builder.Services.AddTransient<IValidator<WorkoutRequestDto>, WorkoutValidator>();
builder.Services.AddTransient<IValidator<ExerciseCreateRequestDto>, ExerciseCreateValidator>();
builder.Services.AddTransient<IValidator<ExerciseUpdateRequestDto>, ExerciseUpdateValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapSwagger().RequireAuthorization();
}

app.UseHttpsRedirection();

app.MapFallbackToFile("/index.html");

app.RegisterWorkoutEndpoints();
app.RegisterExerciseEndpoints();
app.RegisterAuthEndpoints();

app.Run();
