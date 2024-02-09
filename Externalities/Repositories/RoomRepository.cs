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
            return conn.QueryFirst<Room>(@$"
            select from chat_app.rooms where id=@id", id);
        }

        public Room CreateRoom(string name)
        {
            throw new NotImplementedException();
        }

        public Room UpdateRoom(int id, string name)
        {
            throw new NotImplementedException();
        }
        
        public void DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}