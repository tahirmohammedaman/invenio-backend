using System.Text;
using System.Text.Json.Serialization;
using invenio.Data;
using invenio.Repositories;
using invenio.Repositories.Dashboard;
using invenio.Services.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using ODataModelBuilder = invenio.Data.ODataModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddOData(options => 
        options.EnableQueryFeatures().SetMaxTop(null)
            .AddRouteComponents(routePrefix: "api", model: ODataModelBuilder.GetEdmModel())
        );

// Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db context
builder.Services.AddDbContext<InvenioContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

// AutoMapper for DTOs
builder.Services.AddAutoMapper(typeof(Program));

// Mail configuration
var mailConfig = builder.Configuration
    .GetSection("MailConfiguration")
    .Get<MailConfiguration>();
builder.Services.AddSingleton(mailConfig);

builder.Services.AddScoped<IMailSender, MailSender>();


// Cross-origin requests
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// JWT Authentication
builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // ValidIssuer = "https://localhost:5001",
            // ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

// Static files endpoint (for file uploads)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/api/static"
});

app.Environment.IsDevelopment();

app.Run();