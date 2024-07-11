using Microsoft.AspNetCore.Mvc.Infrastructure;
using UberDinner.API.Errors;
using UberDinner.Application;
using UberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


{
    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers(options =>
    //    options.Filters.Add<ErrorHandinglingFilterAttribute>()
    //);
    //Error Handling with Filters

    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, UberDinnerProblemDetailFactory>();

    // Add CORS policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            "AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
        );
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseMiddleware<ErrorHandlingMiddleware>();  //Error Handling Middleware Method
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    // Enable CORS
    app.UseCors("AllowAll");

    app.MapControllers();

    app.Run();
}
