using Externalities.QueryModels;
using Fleck;

namespace api
{
    public class WsWithMetaData(IWebSocketConnection connection)
    {
        public IWebSocketConnection Connection { get; set; } = connection;
        //public string username { get; set; }
        public User? currentUser { get; set; }
        public int currentRoom { get; set; }

        public WsWithMetaData(IWebSocketConnection connection, int currentRoom, User currentUser) : this(connection)
        {
            this.Connection = connection;
            this.currentRoom = currentRoom;
            this.currentUser = currentUser;
        }
    }
    public static class StateService
    {
        public static Dictionary<Guid, WsWithMetaData> Connections = new();
        public static Dictionary<int, HashSet<Guid>> Rooms = new();
        public static Dictionary<User, HashSet<Guid>> Users = new();

        public static bool AddConnection(IWebSocketConnection ws)
        {
            int defaultRoom = 1;
            User currentUser = null!;
            var wsWithMetaData = new WsWithMetaData(ws ,defaultRoom, currentUser);

           return Connections.TryAdd(ws.ConnectionInfo.Id, wsWithMetaData);
        }

        public static bool AddToRoom(IWebSocketConnection ws, int room)
        {   
            if(Connections.TryGetValue(ws.ConnectionInfo.Id, out var wsWithMetaData))
            {
            if(!Rooms.ContainsKey(room))
                Rooms.Add(room, new HashSet<Guid>());
                wsWithMetaData.currentRoom = room;
              return Rooms[room].Add(ws.ConnectionInfo.Id);
            }
            return false;
        }   
        
        public static bool AddUser(IWebSocketConnection ws, User user)
        {
            if(Connections.TryGetValue(ws.ConnectionInfo.Id, out var wsWithMetaData))
            {
                if(!Users.ContainsKey(user))
                    Users.Add(user, new HashSet<Guid>());
                    wsWithMetaData.currentUser = user;
                    Console.WriteLine(wsWithMetaData.currentUser.id + " HERE11111111");
                return Users[user].Add(ws.ConnectionInfo.Id); 
            }
            return false;
        }
        public static bool RemoveFromRoom(IWebSocketConnection ws)
        {
            if(Connections.TryGetValue(ws.ConnectionInfo.Id, out var wsWithMetaData))
            {
                int currentRoom = wsWithMetaData.currentRoom;
                if(Rooms.ContainsKey(currentRoom))
               return Rooms[currentRoom].Remove(ws.ConnectionInfo.Id);

                
            }
            return false;
        }

        public static void BroadCastToRoom(int room, string message)
        {
            if(Rooms.TryGetValue(room, out var guids))
                foreach(var guid in guids)
                {
                    if(Connections.TryGetValue(guid, out var ws))
                        ws.Connection.Send(message);  
                }
        }

    }
}