using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;

    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserId = Guid.NewGuid();

        //SQL query to insert the user into the "Users" table
        string query = "INSERT INTO public.\"Users\" (\"UserId\", \"Email\", " +
            "\"Password\", \"PersonName\", \"Gender\") " +
            "VALUES (@UserId, @Email, @Password, @PersonName, @Gender)";

        // Execute the query using Dapper
        int rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

        if (rowsAffected == 0)
        {
            return null; // Insertion failed
        }
        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        //SQL query to select user by Email and password
        string query = "select * from public.\"Users\" where \"Email\"=@Email and \"Password\"=@Password";
        var parameters = new { Email = email, Password = password };

        ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user;

        //return new ApplicationUser
        //{
        //    UserId = Guid.NewGuid(),
        //    Email = email,
        //    Password = password,
        //    PersonName = "John Doe",
        //    Gender = GenderOptions.Male.ToString()
        //};
    }
}

