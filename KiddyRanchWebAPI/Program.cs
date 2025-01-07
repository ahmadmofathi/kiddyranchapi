using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS Policy Setup
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                "http://localhost",
                "http://localhost:4200",
                "https://localhost:7230",
                "http://localhost:90",
                "http://jupiteracademy-001-site1.ctempurl.com",
                "http://jupiter01academy-001-site2.ctempurl.com",
                "http://kiddyranch.com",
                "https://kiddyranch.com",
                "http://front-testing.kiddyranch.com",
                "https://front-testing.kiddyranch.com"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

// Identity and Authentication Setup
builder.Services.AddIdentity<User, IdentityRole<string>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
}
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

// Connection String Setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Repository and Manager Injection
#region Repo and Manager Injection
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IStudentInterviewRepo, StudentInterviewRepo>();
builder.Services.AddScoped<IStudentInterviewManager, StudentInterviewManager>();
builder.Services.AddScoped<ICareerInterviewRepo, CareerInterviewRepo>();
builder.Services.AddScoped<ICareerInterviewManager, CareerInterviewManager>();
builder.Services.AddScoped<ICallRepo, CallRepo>();
builder.Services.AddScoped<ICallManager, CallManager>();
builder.Services.AddScoped<ISectionManager, SectionManager>();
builder.Services.AddScoped<ISectionRepo, SectionRepo>();
builder.Services.AddScoped<IImageFileManager, ImageFileManager>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IBranchManager, BranchManager>();
builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IIncomeManager, IncomeManager>();
builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
#endregion

// JWT Authentication Setup
var secretKey = builder.Configuration.GetValue<string>("SecretKey");
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentNullException("The secret key cannot be null or empty.");
}
var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
var Key = new SymmetricSecurityKey(secretKeyBytes);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // You might want to validate the issuer in a real scenario
        ValidateAudience = false, // You might want to validate the audience in a real scenario
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = Key,
        ClockSkew = TimeSpan.Zero // To make token expiration time exact
    };
});


var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value?.Equals("/swagger/index.html", StringComparison.OrdinalIgnoreCase) == true)
    {
        context.Response.Redirect("/sorry");
        return;
    }
    await next();
});

//Middleware to Redirect Swagger in Production

// Map the /sorry Route to Serve the Custom HTML Page
app.Map("/sorry", app =>
{
    app.Run(async context =>
    {
        context.Response.ContentType = "text/html";
await context.Response.SendFileAsync(Path.Combine("wwwroot", "custom-pages", "sorry.html"));
    });
});

// Map the /FileManager/GetImage Route to Serve Images
app.Map("/FileManager/GetImage", app =>
{
    app.Run(async context =>
    {
        var fileName = context.Request.Query["ImageName"];

        // Get the file path based on the file name
        var filePath = Path.Combine(Environment.CurrentDirectory, "Uploads", "StaticContent", fileName!);

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Determine the content type based on the file extension
            var contentType = GetContentType(Path.GetExtension(filePath));

            // Serve the file with appropriate content type
            context.Response.ContentType = contentType;
            await context.Response.SendFileAsync(filePath);
        }
        else
        {
            // Return 404 if the file does not exist
            context.Response.StatusCode = 404;
        }
    });
});

// Helper method to get content type based on file extension
string GetContentType(string fileExtension)
{
    return fileExtension.ToLower() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        _ => "application/octet-stream", // Default to binary data
    };
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
}
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization(); // Place this after UseAuthentication() and before UseEndpoints()

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
