using System.Data;
using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;
using Models.DTO;
using DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql.Replication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DbContext;

//DbContext namespace is a fundamental EFC layer of the database context and is
//used for all Database connection as well as for EFC CodeFirst migration and database updates 

public class MainDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    IConfiguration _configuration;
    DatabaseConnections _databaseConnections;

    public string dbConnection  => _databaseConnections.GetDbConnection(this.Database.GetConnectionString());

    #region C# model of database tables
    public DbSet<MoodDbM> Moods { get; set; }    
    public DbSet<ActivityDbM> Activities { get; set; }    
    public DbSet<StaffDbM> Staffs { get; set; }    
    public DbSet<AppetiteDbM> Appetites { get; set; }    
    public DbSet<SleepDbM> Sleeps { get; set; }  
     public DbSet<MoodKindDbM> MoodKinds { get; set; }    
    public DbSet<PatientDbM> Patients{ get; set; }     
    public DbSet<UserDbM> Users { get; set; }    
    public DbSet<GraphDbM> Graphs { get; set; }    
    #endregion


    #region constructors
    public MainDbContext() { }
    public MainDbContext(DbContextOptions options, IConfiguration configuration, DatabaseConnections databaseConnections) : base(options)
    { 
        _databaseConnections = databaseConnections;
        _configuration = configuration;
    }
    #endregion

    public DbSet<GstUsrInfoDbDto> InfoDbView { get; set; }
    public DbSet<GstUsrInfoStaffsDto> InfoStaffsView { get; set; }

    //Here we can modify the migration building
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     modelBuilder.Entity<GstUsrInfoDbDto>().ToView("vwInfoDb", "gstusr").HasNoKey();
     modelBuilder.Entity<GstUsrInfoStaffsDto>().ToView("vwInfoStaffs", "gstusr").HasNoKey();   

      modelBuilder.Entity<MoodKind>()
            .Ignore(mk => mk.Mood);  // Ignore the navigation property 'Mood'
     modelBuilder.Ignore<IPatient>(); // 🚀 Ensure IPatient is ignored
     
     
         base.OnModelCreating(modelBuilder);
    }

    #region DbContext for some popular databases
    public class SqlServerDbContext : MainDbContext
    {
        public SqlServerDbContext() { }
        public SqlServerDbContext(DbContextOptions options, IConfiguration configuration, DatabaseConnections databaseConnections) 
            : base(options, configuration, databaseConnections) { }


        //Used only for CodeFirst Database Migration and database update commands
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = SetupDatabaseConnection(optionsBuilder, 
                    (options, connectionString) => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HaveColumnType("money");
            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(200)");

            base.ConfigureConventions(configurationBuilder);
        }
    }

    public class MySqlDbContext : MainDbContext
    {
        public MySqlDbContext() { }
        public MySqlDbContext(DbContextOptions options) : base(options, null, null) { }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = SetupDatabaseConnection(optionsBuilder, 
                    (options, connectionString) => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");

            base.ConfigureConventions(configurationBuilder);

        }
    }

    public class PostgresDbContext : MainDbContext
    {
        public PostgresDbContext() { }
        public PostgresDbContext(DbContextOptions options) : base(options, null, null){ }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = SetupDatabaseConnection(optionsBuilder, 
                    (options, connectionString) => options.UseNpgsql(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");
            base.ConfigureConventions(configurationBuilder);
        }
    }

    public class SqliteDbContext : MainDbContext
    {
        public SqliteDbContext() { }
        public SqliteDbContext(DbContextOptions options) : base(options, null, null){ }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = SetupDatabaseConnection(optionsBuilder, 
                    (options, connectionString) => options.UseSqlite(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
    #endregion


    #region Methods used only during Migration
    private void DesignTimeMigrationCreateServices()
    {
        //ASP.NET Core program.cs has not run by efc design-time, configure and create services as in 
        //program.cs

        // Create a configuration
        System.Console.WriteLine($"   configuring");
        var conf = new ConfigurationBuilder();
        conf.AddApplicationSecrets("../Configuration/Configuration.csproj");

        System.Console.WriteLine($"   building configuring");
        var configuration = conf.Build();

        System.Console.WriteLine($"   creating services");
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDatabaseConnections(configuration);
        serviceCollection.AddSingleton<IConfiguration>(configuration);

        // Build the service provider and get the DbContext
        System.Console.WriteLine($"   retrieving services from serviceProvider");

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        System.Console.WriteLine($"   {nameof(IConfiguration)} retrieved");

        _databaseConnections = serviceProvider.GetRequiredService<DatabaseConnections>();
        System.Console.WriteLine($"   {nameof(DatabaseConnections)} retrieved");
        System.Console.WriteLine($"      secret source: {_databaseConnections.SetupInfo.SecretSource}");
        System.Console.WriteLine($"      database server: {_databaseConnections.SetupInfo.DataConnectionServer}");
        System.Console.WriteLine($"      database connection tag: {_databaseConnections.SetupInfo.DataConnectionTag}");

    }

    private DbConnectionDetail GetDatabaseConnection()
    {
        var connection = _databaseConnections.GetDataConnectionDetails(_configuration["DatabaseConnections:MigrationDataUser"]);
        if (connection.DbConnectionString == null)
        {
            throw new InvalidDataException($"Error: Connection string for {connection.DbConnection}, {connection.DbUserLogin} not set");
        }

        return connection;
    }

    DbContextOptionsBuilder SetupDatabaseConnection(DbContextOptionsBuilder optionsBuilder, Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> options)
    {
        System.Console.WriteLine($"{nameof(SqlServerDbContext)}: executing OnConfiguring...");
        if (_databaseConnections == null || _configuration == null)
        {
            DesignTimeMigrationCreateServices();
        }

        DbConnectionDetail connection = GetDatabaseConnection();

        optionsBuilder = options(optionsBuilder, connection.DbConnectionString);
        System.Console.WriteLine($"{nameof(SqlServerDbContext)}: OnConfiguring completed successfully");
        System.Console.WriteLine($"Proceeding with migration with user: {connection.DbUserLogin} " +
                                        $"on database connection: {connection.DbConnection}");
        return optionsBuilder;
    }
    #endregion
}

