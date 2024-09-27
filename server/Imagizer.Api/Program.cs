using Imagizer.Api;
using Imagizer.Api.Infrastructure.FileUpload;
using Imagizer.Api.Infrastructure.LinkShortener;
using Imagizer.Api.Options.CorsOrigin;
using Imagizer.Api.Options.OpenApi;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureOptions<ConfigureShortenerOptions>();
builder.Services.ConfigureOptions<ConfigureMinioOptions>();
builder.Services.ConfigureOptions<ConfigureOpenApiOptions>();
builder.Services.AddCors(options =>
{
    var corsOriginOptions = builder
        .Configuration.GetSection(nameof(CorsOriginOptions))
        .GetChildren()
        .ToArray();

    options.AddPolicy("default", corsPolicy => { corsPolicy.WithOrigins(corsOriginOptions[0].Value!); });
});
builder.Services.ConfigureSwaggerGen(builder.Configuration);
builder.Services.ConfigureRateLimiter();
builder.Services.ConfigureSwaggerGen(options => { options.AddEnumsWithValuesFixFilters(); });
builder.Services.ConfigureServices(builder.Environment);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("default");
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");

await app.RunAsync();