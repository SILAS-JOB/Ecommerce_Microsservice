using System.Security;
using Dapper;
using Ecommerce.Core.DTO;
using Ecommerce.Core.Entities;
using Ecommerce.Core.RepositoryContracts;
using Ecommerce.Infrastructure.DbContext;

namespace Ecommerce.Infrastructure.Repositories;

internal class UserRepository : IUsersRepository
{
    private readonly DapperDbContext _dbContext;

    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();   

        string query = 
        "INSERT INTO public. \"Users\"(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") VALUES(@UserID, @Email, @Person, @Gender, @Password)";

        int rowsCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);
        

        if(rowsCountAffected > 0)
        {
            return user;
        }
        else
        {
            return null;
        }

    }

    public async Task<ApplicationUser> GetUserByEmailAndPassword(string? email, string? password)
    {
        string query = "SELECT * FROM .\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";

        var parameters = new { Email = email, Password = password,  };




        ApplicationUser user = 
        await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);


        return new ApplicationUser()
        {
            UserID = Guid.NewGuid(),
            Email = email,
            Password = password,
            PersonName = "Person Name",
            Gender = GenderOptions.Male.ToString()
        }; 
    }
}