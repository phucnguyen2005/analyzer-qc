using System.Text;
using AnalyzerQC.Application;
using AnalyzerQC.Infrastructure;
using AnalyzerQC.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:Key"])),
        /*ValidateIssuerSigningKey = true,*/
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services));

builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });


    // Define the security scheme (e.g., Bearer Authentication)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

builder.Services.AddScoped<IAnalyzerService, AnalyzerService>();
builder.Services.AddScoped<IModelGroupService, ModelGroupService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IAssayLimitService, AssayLimitService>();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<AuditInterceptor>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>((sp, optionsBuilder) =>
{
    var interceptor = sp.GetRequiredService<AuditInterceptor>();

    optionsBuilder.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(interceptor);
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();