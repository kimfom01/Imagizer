using System.Reflection;
using Imagizer.Api.Services;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Imagizer API",
        Description = "A Web API for manipulating images",
        Contact = new OpenApiContact
        {
            Name = "Kim Fom",
            Email = "kimfom01@gmail.com",
            Url = new Uri("https://kimfom.space")
        },
        License = new OpenApiLicense
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.ConfigureSwaggerGen(options => { options.AddEnumsWithValuesFixFilters(); });
builder.Services.AddScoped<IImageProcessorService, ImageProcessorService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();