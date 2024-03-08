using Dapper;
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
            using var conn = _dataSource.OpenConnection();
            return conn.Query<User>(@$"
            select * from chat_app.users;
            ");
        }

        public User GetUserById(int id)
        {
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<User>(@$"
            select from chat_app.users where id=@id;
            ", id);
        }

        public User FindUserByUsername(string nickname)
        {
            using var conn = _dataSource.OpenConnection();
            var paramaters = new {nickname};
            return conn.QueryFirstOrDefault<User>(@$"
            select nickname as {nameof(User.username)} from chat_app.users where nickname=@nickname;
            ", paramaters)! ?? throw new KeyNotFoundException("Could not find user with username " + nickname);
        }

         public User CreateUser(string username)
        {
            var paramaters = new {username};
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<User>(@$"
            insert into chat_app.users (nickname) values (@username)
            returning *;
            ", paramaters);
        }

        public User UpdateUser(int id, string username)
        {
            var paramaters = new {id, username};
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<User>(@$"
            update into chat_app.users set nickname=@username where id=@id
            returning *;
            ", paramaters);
        }

        public void DeleteUser(int id)
        {
            using var conn = _dataSource.OpenConnection();
            conn.Execute($@"
                delete from chat_app.users where id=@id;
            ", id);
        }

        
    }
}