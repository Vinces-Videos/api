using Database;
using Database.Mongo;
using Microsoft.AspNetCore.Diagnostics;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency inject our IDatabaseController into the project - this could be extended to inject different database providers.
switch (builder.Configuration["DatabaseType"])
{
    case "Mongo":
    {
        builder.Services.AddSingleton<IDatabaseContext, MongoDbContext>();
        builder.Services.AddTransient<IDatabaseValidations, MongoDatabaseValidations>();
        break;
    }
    //Add support for other databases here
}

// We add the repository as a singleton so we don't rebuild the cache.
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
builder.Services.AddSingleton<IRentalsRepository, RentalsRepository>();
builder.Services.AddSingleton<IInvoicesRepository, InvoicesRepository>();
builder.Services.AddSingleton<ICustomersRepository, CustomersRepository>();

// We can add the services as transient which will allow them to spun up per request or reused as per ASP.NET's will.
builder.Services.AddTransient<IRentalsService, RentalsService>();
builder.Services.AddTransient<IInvoicesService, InvoicesService>();
builder.Services.AddTransient<ICustomersService, CustomersService>();
builder.Services.AddTransient<IProductsService, ProductsService>();

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
