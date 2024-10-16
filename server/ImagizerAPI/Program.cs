using ImagizerAPI;
using ImagizerAPI.Infrastructure.FileUpload;
using ImagizerAPI.Infrastructure.LinkShortener;
using ImagizerAPI.Options.CorsOrigin;
using ImagizerAPI.Options.OpenApi;
using Scalar.AspNetCore;
using ServiceDefaults;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureOptions<ConfigureShortenerOptions>();
builder.Services.ConfigureOptions<ConfigureAzureBlobOptions>();
builder.Services.ConfigureOptions<ConfigureOpenApiOptions>();
builder.Services.AddCors(options =>
{
    var corsOrigins = builder
        .Configuration.GetSection(nameof(CorsOriginOptions))
        .GetChildren()
        .Select(x => x.Value!)
        .ToArray();

    options.AddPolicy("default", corsPolicy =>
    {
        corsPolicy.WithOrigins(corsOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.ConfigureSwaggerGen();
builder.Services.ConfigureRateLimiter();
builder.Services.ConfigureSwaggerGen(options => { options.AddEnumsWithValuesFixFilters(); });
builder.Services.ConfigureServices();

var app = builder.Build();

app.MapScalarApiReference();

app.UseSwagger(options => { options.RouteTemplate = "openapi/{documentName}.json"; });
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("default");
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();

app.MapGet("/", () => new { message = "Hello from Imagizer API" })
    .ExcludeFromDescription();

await app.RunAsync();