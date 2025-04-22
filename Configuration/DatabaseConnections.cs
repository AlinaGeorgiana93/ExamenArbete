using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Configuration;

public enum DatabaseServer { SQLServer, MySql, PostgreSql, SQLite }
public class DatabaseConnections
{
    readonly IConfiguration _configuration;
    readonly DbConnectionSetsOptions _options;
    private readonly DbSetDetailOptions _activeDataSet;

    public SetupInformation SetupInfo => new SetupInformation()
    {

        SecretSource = _configuration.GetValue<bool>("ApplicationSecrets:UseAzureKeyVault")
                ? $"Azure: {Environment.GetEnvironmentVariable("AZURE_KeyVaultSecret")}"
                : $"Usersecret: {Environment.GetEnvironmentVariable("USERSECRETID")}",

        DefaultDataUser = _configuration["DatabaseConnections:DefaultDataUser"],
        MigrationDataUser = _configuration["DatabaseConnections:MigrationDataUser"],

        DataConnectionTag = _activeDataSet.DbTag,

        DataConnectionServer = _activeDataSet.DbServer.Trim().ToLower() switch
        {
            "sqlserver" => DatabaseServer.SQLServer,
            "mysql" => DatabaseServer.MySql,
            "postgresql" => DatabaseServer.PostgreSql,
            "sqlite" => DatabaseServer.SQLite,
            _ => throw new NotSupportedException($"DbServer {_activeDataSet.DbServer} not supported")
        },

    };
   
    public DbConnectionDetail GetDataConnectionDetails(string user) => GetLoginDetails(user, _activeDataSet);

   DbConnectionDetail GetLoginDetails(string user, DbSetDetailOptions dataSet)

   
{  Console.WriteLine($"Looking for user: '{user}'");
   Console.WriteLine($"Available users: {string.Join(", ", dataSet.DbConnections.Select(c => $"'{c.DbUserLogin}'"))}");

    if (string.IsNullOrEmpty(user) || string.IsNullOrWhiteSpace(user))
        throw new ArgumentNullException(nameof(user));

    // Use FirstOrDefault instead of First
    var conn = dataSet.DbConnections.FirstOrDefault(m => m.DbUserLogin.Trim().ToLower() == user.Trim().ToLower());

    if (conn == null)
    {
        // Handle the case where no matching user was found.
        // You can log, return null, or throw a more specific exception.
        throw new InvalidOperationException($"No database connection found for user: {user}");
    }

    Console.WriteLine($"Available users: {string.Join(", ", _activeDataSet.DbConnections.Select(c => c.DbUserLogin))}");

    return new DbConnectionDetail
    {
        DbUserLogin = conn.DbUserLogin,
        DbConnection = conn.DbConnection,
        DbConnectionString = _configuration.GetConnectionString(conn.DbConnection)
    };

}


    //Not to revieal the connection string, I find the corresponding DbConnectionKey in the 
   public string GetDbConnection(string DbConnectionString)
{
    var connection = _activeDataSet.DbConnections
        .FirstOrDefault(m => _configuration.GetConnectionString(m.DbConnection).Trim().ToLower() == DbConnectionString.Trim().ToLower());

    if (connection == null)
    {
        // Handle the case where no matching DbConnection is found
        throw new InvalidOperationException($"No matching database connection found for connection string: {DbConnectionString}");
    }

    return connection.DbConnection;
}


    public DatabaseConnections(IConfiguration configuration, IOptions<DbConnectionSetsOptions> dbSetOption)
    {
        _configuration = configuration;
        _options = dbSetOption.Value;

        _activeDataSet = _options.DataSets.FirstOrDefault(ds => ds.DbTag.Trim().ToLower() == configuration["DatabaseConnections:UseDataSetWithTag"].Trim().ToLower());
        if (_activeDataSet == null)
            throw new ArgumentException($"Dataset with DbTag {configuration["DatabaseConnections:UseDataSetWithTag"]} not found");
    }
   

    public class SetupInformation
    {
        public string AppEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public string SecretSource { get; init; }
        public string DataConnectionTag { get; init; }
        public string DefaultDataUser { get; init; }
        public string MigrationDataUser { get; init; }
        public DatabaseServer DataConnectionServer { get; init; }
        public string DataConnectionServerString => DataConnectionServer.ToString();  //for json clear text
    }
    
}