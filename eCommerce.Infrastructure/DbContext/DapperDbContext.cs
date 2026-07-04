using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace eCommerce.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;

    public DapperDbContext(IConfiguration configuration)
    {
        //TO DO: Initialize the database connection here
        _configuration = configuration;
        string? connectionString = _configuration.GetConnectionString("PostgreSqlConnection");

        //Create a new NpgsqlConnection using the connection string
        _connection = new NpgsqlConnection(connectionString);
    }

    public IDbConnection DbConnection => _connection;
}
