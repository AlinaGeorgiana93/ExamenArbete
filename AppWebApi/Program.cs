using Configuration;
using DbContext;
using DbRepos;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();

#region adding support for several secret sources and database sources
//to use either user secrets or azure key vault depending on UseAzureKeyVault tag in appsettings.json
builder.Configuration.AddApplicationSecrets("../Configuration/Configuration.csproj");

// Debugging line to check if the connection string is correctly loaded
var connectionString = builder.Configuration.GetConnectionString("SQLServer-graphefc-docker-sysadmin");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Connection string is empty or not found.");
}
else
{
    Console.WriteLine($"Connection String: {connectionString}");
}
//use multiple Database connections and their respective DbContexts
builder.Services.AddEncryptions(builder.Configuration);
builder.Services.AddDatabaseConnections(builder.Configuration);
builder.Services.AddDatabaseConnectionsDbContext();
#endregion

builder.Services.AddJwtTokenService(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Inject Custom logger - grouped by two to be easier to find if we put all of them 
builder.Services.AddSingleton<ILoggerProvider, InMemoryLoggerProvider>();

builder.Services.AddScoped<AdminDbRepos>();
builder.Services.AddScoped<IAdminService, AdminServiceDb>();

builder.Services.AddScoped<LoginDbRepos>();
builder.Services.AddScoped<ILoginService, LoginServiceDb>();

builder.Services.AddScoped<MoodDbRepos>();
builder.Services.AddScoped<IMoodService, MoodServiceDb>();

builder.Services.AddScoped<MoodKindDbRepos>();
builder.Services.AddScoped<IMoodKindService, MoodKindServiceDb>();

builder.Services.AddScoped<SleepDbRepos>();
builder.Services.AddScoped<ISleepService, SleepServiceDb>();

builder.Services.AddScoped<SleepLevelDbRepos>();
builder.Services.AddScoped<ISleepLevelService, SleepLevelServiceDb>();

builder.Services.AddScoped<StaffDbRepos>();
builder.Services.AddScoped<IStaffService, StaffServiceDb>();

builder.Services.AddScoped<PatientDbRepos>();
builder.Services.AddScoped<IPatientService, PatientServiceDb>();

builder.Services.AddScoped<GraphDbRepos>();
builder.Services.AddScoped<IGraphService, GraphServiceDb>();

builder.Services.AddScoped<AppetiteDbRepos>();
builder.Services.AddScoped<IAppetiteService, AppetiteServiceDb>();

builder.Services.AddScoped<AppetiteLevelDbRepos>();
builder.Services.AddScoped<IAppetiteLevelService, AppetiteLevelServiceDb>();

builder.Services.AddScoped<PatientDbRepos>();
builder.Services.AddScoped<IPatientService, PatientServiceDb>();

builder.Services.AddScoped<ActivityDbRepos>();
builder.Services.AddScoped<IActivityService, ActivityServiceDb>();

builder.Services.AddScoped<ActivityLevelDbRepos>();
builder.Services.AddScoped<IActivityLevelService, ActivityLevelServiceDb>();

builder.Services.AddScoped<StaffDbRepos>();
builder.Services.AddScoped<IStaffService, StaffServiceDb>();

builder.Services.AddScoped<PasswordResetTokenRepos>();
builder.Services.AddScoped<IPasswordResetTokenService, PasswordResetTokenServiceDb>();


var app = builder.Build();

//Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// global cors policy - the call to UseCors() must be done here
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:3000")
    .AllowCredentials());

app.UseAuthorization();
app.MapControllers();

app.Run();
