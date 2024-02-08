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
            throw new NotImplementedException();
        }

        public Room GetRoomById(int id)
        {
            throw new NotImplementedException();
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