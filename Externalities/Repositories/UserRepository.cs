using Externalities.Interfaces;
using Externalities.QueryModels;
using Npgsql;

namespace Externalities.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly NpgsqlDataSource _dataSource;

        public UserRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

         public User CreateUser(string username)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(int id, string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}