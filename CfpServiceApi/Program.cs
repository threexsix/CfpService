using CfpService;
using CfpService.Application;
using CfpService.Application.Extensions;
using CfpService.Extensions;
using CfpService.Infrastructure;
using CfpService.Infrastructure.Extension;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.MigrateUp();
    app.Run();
}