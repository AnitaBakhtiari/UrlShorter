using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UrlShorter.Data;
using UrlShorter.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrlShorter", Version = "v1" });
});
builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
//sqlite database
//services.AddDbContext<ServerContext>(options => options.UseSqlite("filename=ShortUrlDb.db"));

//sql server database
builder.Services.AddDbContext<ServerContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddResponseCaching();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();
