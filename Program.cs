using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.MiddleWares;
using NZWalks.API.Repositories;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("Logs/NZWalks_log.txt"  ,rollingInterval : RollingInterval.Minute )
	.MinimumLevel.Information()
	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<NZWalksDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
builder.Services.AddScoped<HttpContextAccessor>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
	.AddEntityFrameworkStores<NZWalksDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<IdentityOptions>(option =>
{
	option.Password.RequireDigit = false;
	option.Password.RequireLowercase = false;
	option.Password.RequireUppercase = false;
	option.Password.RequireNonAlphanumeric = false;
	option.Password.RequiredUniqueChars = 1;
	option.Password.RequiredLength = 6;
});

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(optoin => optoin.TokenValidationParameters = new TokenValidationParameters
{
		ValidateIssuer = true,
		ValidateAudience =true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
	
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
	
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
	RequestPath = "/Image"
});
 
app.MapControllers();

app.Run();
