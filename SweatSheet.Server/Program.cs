using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SweatSheet.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite("Data Source=workouts.db"),
    ServiceLifetime.Singleton
);

builder.Services.AddTransient<WorkoutsService>();
builder.Services.AddTransient<ExercisesService>();

builder.Services.AddTransient<IValidator<WorkoutRequestDto>, WorkoutValidator>();
builder.Services.AddTransient<IValidator<ExerciseCreateRequestDto>, ExerciseCreateValidator>();
builder.Services.AddTransient<IValidator<ExerciseUpdateRequestDto>, ExerciseUpdateValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapFallbackToFile("/index.html");

app.RegisterWorkoutEndpoints();
app.RegisterExerciseEndpoints();

app.Run();