global using StudentManagementSystem.Models;
global using StudentManagementSystem.Data;
using StudentManagementSystem.Services.StudentManagementServices;
using StudentManagementSystem.Services.AdminManagementServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using StudentManagementSystem.Services;
using StudentManagementSystem.Services.EmailServices;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.AllowAnyOrigin();
//            builder.AllowAnyMethod();
//            builder.AllowAnyHeader();
//        });
//    /*    options.AddPolicy("SignalRPolicy", builder =>
//        {
//            builder.WithOrigins()
//            .AllowCredentials()
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .SetIsOriginAllowed(_ => true);
//        });*/
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Standard authorization using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddDbContext<StudentDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentManagementServices, StudentManagementServices>();
builder.Services.AddScoped<IAdminManagementServices, AdminManagementServices>();

// added this one
builder.Services.AddCors(options =>
{
    // options.AddPolicy(corsPolicy, policy => polic
    options.AddPolicy("AllowSpecificOrigin", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Configure EmailSettings from appsettings.json
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register EmailService
builder.Services.AddTransient<IEmailServices, EmailServices>();

//builder.Services.AddTransient<IEmailServices>(provider =>
//new EmailServices("smtp.gmail.com", 587, "anetuniversity@gmail.com", "your-email-password"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // added this one
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
