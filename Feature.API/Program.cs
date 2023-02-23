using Feature.API.ExceptionMiddleware;
using Feature.API.Logger;
using Feature.API.SeriLogMiddleware;
using Feature.JWT;
using Feature.JWT.Context;
using Feature.JWT.Interface;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Feature.BusinessModel.Common;
using Feature.Services.Abstract;
using Feature.Services.Concrete;
using Feature.Repository.Interface.Interfaces;
using Feature.Repository.DBFirst.Repositories;
using Feature.Repository.DBFirst.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Feature.Entity.Entities;
using Feature.Repository.Interface.Generic;
using Feature.Repository.DBFirst.Generic;
using Feature.Repository.Dapper.Repositories;
using Feature.Repository.Dapper.Generic;
using Amazon.S3;
using Feature.AWS.Storage.Interfaces;
using Feature.AWS.Storage.Repository;
using Feature.Repository.CodeFirst.Generic;
using Feature.Repository.CodeFirst.Repositories;
using Feature.Repository.Interface.ContextInterface;
using Feature.Repository.CodeFirst.Context;
using AutoMapper;
using Feature.Mapper;
using Feature.Azure.Storage;
using FluentValidation.AspNetCore;
using Feature.API.Extention;
using Feature.BusinessModel.Validators;
using Feature.API;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Feature.DAL.Repositories.Interfaces;
using Feature.DAL.Repositories.Repositories;
using Feature.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// implementing swagger
string result = builder.Configuration.GetValue<string>("jwtauth");
if (result == "true")
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Feature API, Resuable component",
            Description = "You can have different reusable libraries and also can you this API for your project.",

        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("appsettings").GetSection("Secret").Value);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        //x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
        };
    });
    ////implementing Openid Authentication
    //string[] initialScopes = builder.Configuration.GetValue<string>("MicrosoftGraph:Scopes")?.Split(' ');

    //builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme);

    //var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("appsettings").GetSection("Secret").Value);
    //builder.Services.AddAuthentication(x =>
    //{
    //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //}).AddJwtBearer(x =>
    //{
    //    //x.RequireHttpsMetadata = false;
    //    x.SaveToken = true;
    //    x.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(key),
    //        ValidateIssuer = false,
    //        ValidateAudience = false,
    //        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
    //        ClockSkew = TimeSpan.Zero
    //    };
    //});
}
else
{
    //implementing Openid Authentication
    string[] initialScopes = builder.Configuration.GetValue<string>("MicrosoftGraph:Scopes")?.Split(' ');

    builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)

        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))

        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)

        .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))

        .AddInMemoryTokenCaches();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Feature API, Resuable component",
            Description = "You can have different reusable libraries and also can you this API for your project.",

        });
    });
}
// implementing NLog
builder.Services.AddTransient<ILoggerExtention, LoggerExtention>();



// implementiong CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CIOApi",
       builder => builder.WithOrigins("http://localhost:5008", "http://3.144.238.31")
           .AllowAnyHeader()
           .AllowAnyMethod());



});

// implementing validations
//builder.Services.AddControllers()
//                .AddFluentValidation(options =>
//                {
//                    // Validate child properties and root collection elements
//                    options.ImplicitlyValidateChildProperties = true;
//                    options.ImplicitlyValidateRootCollectionElements = true;

//                    // Automatic registration of validators in assembly
//                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//                });

//Implementing validation on models globally.
builder.Services.AddControllers()
    .AddFluentValidation(s =>
    {
   
        s.RegisterValidatorsFromAssemblyContaining<UserDetailsValidator>();
        
    });

//implementing Serilog
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//implementing builtin Logging functions
builder.Logging.AddConsole();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("appsettings"));
builder.Services.AddDbContext<JWTDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddTransient<IJWTDBContext, JWTDBContext>();
builder.Services.AddTransient<IJWTTokenConfig, JWTTokenConfig>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient <IGenericRepository<UserProfile>, GenericRepository<UserProfile>>();
// Dapper
builder.Services.AddTransient<IGenericRepositoryDapper<UserProfile>, GenericRepositoryDapper<UserProfile>>();
builder.Services.AddTransient<IUserRepositoryDapper, UserRepositoryDapper>();
builder.Services.AddScoped<IUserServiceDapper, UserServiceDapper>();
builder.Services.AddTransient <IGenericRepository<UserProfile>, GenericRepository<UserProfile>>();
//Aws S3 Bucket implementation
builder.Services.Configure<AwsSettings>(builder.Configuration.GetSection("AWS"));
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<IBucketService, BucketService>();
builder.Services.AddTransient<IBucketStorage, BucketStorage>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFileStorage, FileStorage>();
//var dbClient = new AmazonDynamoDBClient(new StoredProfileAWSCredentials(),
                     //RegionEndpoint.APSouth1);

//CodeFirst
builder.Services.AddTransient<IGenericCodeFirstRepository<Role>, GenericCodeFirstRepository<Role>>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddTransient<ICodeFirstDbContext, CodeFirstDbContext>();
builder.Services.AddDbContext<CodeFirstDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

//Azure
builder.Services.AddScoped<IAzureStorageRepository, AzureStorageRepository>();
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();
builder.Services.AddScoped<IAzureStorage, AzureStorage>();

//DAL
builder.Services.ConfigureService();
builder.Services.AddScoped<IDALRepository, DALRepository>();
builder.Services.AddScoped<IDALService, DALService>();

//Mapper
builder.Services.AddScoped<IRoleMapperService, RoleMapperService>();
builder.Services.AddScoped<IUserMapperService, UserMapperService>();

//validations
builder.Services.AddScoped<ValidationFilter>();

//HealthCheck

// Basic Healthcheck 
builder.Services.AddHealthChecks()
    //Database Health check
    .AddSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
    //Custom Health Checks
    .AddCheck<MyHealthCheck>("MyHealthCheck");
//Health Checks UI
// builder.Services.AddHealthChecksUI().AddInMemoryStorage();

// builder.Services.AddHealthChecksUI(opt =>
//{
//    opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
//    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
//    opt.SetApiMaxActiveRequests(1); //api requests concurrency    
//    opt.AddHealthCheckEndpoint("default api", "/api/health"); //map health check api    
//})
//            .AddInMemoryStorage();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new RoleProfileMapper());
    mc.AddProfile(new UserProfileMapper());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMvc();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qentelli Reusable components");
});
//implementing Global Exception Handling
app.UseMiddleware<GlobalExceptionMiddleware>();
//app.UseMiddleware<JwtMiddleware>();

//implementing Serilog
app.UseMiddleware(typeof(SerilogMiddleware));
if(result == "true")
{
    app.ValidateJWTToken();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseCors("CIOApi");
app.UseAuthorization();
//app.MapHealthChecks();
app.MapHealthChecks("/health/secure", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).RequireAuthorization()
.RequireCors("CIOApi"); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});
// implementing bultin logging function
app.MapRazorPages();
//app.MapControllers();
app.Run();