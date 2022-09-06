using Database;
using Database.Mongo;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";
// Add services to the container.

builder.Services.AddCors(options => {
    options.AddPolicy(name: AllowSpecificOrigins,
    policy => 
    {
        policy.WithOrigins("http://localhost:3000", "http://192.168.1.234:3000");
    });
});

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

var cacheOptions = new MemoryCacheOptions
{   
    SizeLimit = 1000,
    ExpirationScanFrequency = TimeSpan.FromSeconds(10)
};

// We add the repository as a singleton so we don't rebuild the cache.
builder.Services.AddSingleton<IDatabaseItemRepository<Product>, DatabaseItemRepository<Product>>(provider => 
        new DatabaseItemRepository<Product>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

builder.Services.AddSingleton<IDatabaseItemRepository<Rental>, DatabaseItemRepository<Rental>>(provider => 
        new DatabaseItemRepository<Rental>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

builder.Services.AddSingleton<IDatabaseItemRepository<Invoice>, DatabaseItemRepository<Invoice>>(provider => 
        new DatabaseItemRepository<Invoice>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

builder.Services.AddSingleton<IDatabaseItemRepository<Customer>, DatabaseItemRepository<Customer>>(provider => 
        new DatabaseItemRepository<Customer>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

builder.Services.AddSingleton<IDatabaseItemRepository<Confectionary>, DatabaseItemRepository<Confectionary>>(provider => 
        new DatabaseItemRepository<Confectionary>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

builder.Services.AddSingleton<IDatabaseItemRepository<FilmCategory>, DatabaseItemRepository<FilmCategory>>(provider => 
        new DatabaseItemRepository<FilmCategory>(
            dbController: provider.GetService<IDatabaseContext>(), 
            options: cacheOptions
            ));

// We can add the services as transient which will allow them to spun up per request or reused as per ASP.NET's will.
builder.Services.AddTransient<IRentalsService, RentalsService>();
builder.Services.AddTransient<IInvoicesService, InvoicesService>();
builder.Services.AddTransient<ICustomersService, CustomersService>();
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IConfectionaryService, ConfectionaryService>();
builder.Services.AddTransient<IFilmCategoryService, FilmCategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

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
