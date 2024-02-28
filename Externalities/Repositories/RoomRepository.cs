using Dapper;
using Externalities.Interfaces;
using Externalities.QueryModels;
using Npgsql;

namespace Externalities.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public RoomRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }



        public IEnumerable<Room> GetAllRooms()
        {
            using var conn = _dataSource.OpenConnection();
            return conn.Query<Room>(@$"
            select * from chat_app.rooms;
            ");
        }

        public Room GetRoomById(int id)
        {
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirstOrDefault<Room>(@$"
            select * from chat_app.rooms where id=@id;", new { id })!;
        }

        public Room CreateRoom(int id, string name)
        {
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<Room>(@$"
            insert into chat_app.rooms (id ,name) values (@id ,@name)
            returning *;
            ", new { id, name });
        }

        public Room UpdateRoom(int id, string name)
        {
            var paramaters = new { id, name };
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<Room>(@$"
            update chat_app.rooms set name=@name where id=@id
            returning *;
            ", paramaters);
        }

        public void DeleteRoom(int id)
        {
            using var conn = _dataSource.OpenConnection();
            conn.Execute($@"
            delete from chat_app.rooms where id=@id;
            ", id);
        }


    }
}