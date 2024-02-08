using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Externalities.QueryModels;

namespace Externalities.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(int id);
        Room CreateRoom(string name);
        Room UpdateRoom(int id, string name);
        void DeleteRoom(int id);
    }
}