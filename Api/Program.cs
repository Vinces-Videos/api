using Database;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dependency inject our IDatabaseController into the project - this could be extended to inject different database providers.
builder.Services.AddSingleton<IDatabaseContext, MongoDbContext>();
builder.Services.AddSingleton<IDatabaseValidations, MongoDatabaseValidations>();

// We add the repository as a singleton so we don't rebuild the cache.
builder.Services.AddSingleton<Repositories.IProducts, Repositories.Products>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Exception middleware - Using a lambda allows access to the error before returning the response.
// Means we don't have to write exception handling on individual endpoints, instead we can do it here, globally.
app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is KeyNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("This id was not found.");
            }
        });
    });

app.Run();
