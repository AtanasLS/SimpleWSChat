using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Externalities.Interfaces;
using Externalities.QueryModels;
using Npgsql;

namespace Externalities.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public MessageRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            using var conn = _dataSource.OpenConnection();
            return conn.Query<Message>(@$"
            select
            content as {nameof(Message.content)},
            timestamp as {nameof(Message.timestapm)},
            user_id as {nameof(Message.userId)},
            username as {nameof(Message.user)},
            room_id as {nameof(Message.roomId)},
            room as {nameof(Message.room)}
            FROM chat_app.messages
            join chat_app.users user on chat_app.messages = chat_app.users.id
            join chat_app.rooms room on chat_app.messages = chat_app.rooms.id;
            ");
        }

        public Message GetMessageById(int id)
        {
            using var conn = _dataSource.OpenConnection();
            return conn.QueryFirst<Message>($@"
            select
            content as {nameof(Message.content)},
             timestamp as {nameof(Message.timestapm)},
            user_id as {nameof(Message.userId)},
            username as {nameof(Message.user)},
            room_id as {nameof(Message.roomId)},
            room as {nameof(Message.room)}
            FROM chat_app.messages
            join chat_app.users user on chat_app.messages = chat_app.users.id
            join chat_app.rooms room on chat_app.messages = chat_app.rooms.id
            where chat.app.id = @id;
            ", id);
        }

        public Message CreateMessage(string content, DateTimeOffset timestamp, int userId, int roomId)
        {
            
            using( var conn = _dataSource.OpenConnection()){
                 
            var paramaters = new {content, timestamp, userId, roomId};
            
            return conn.QueryFirst<Message>(@$"
            insert into chat_app.messages (content, timestamp, user_id, room_id)
            values (@content, @timestamp, @userId, @roomId)
            Returning *;
            ", paramaters);
            }
           
        }

        public Message UpdateMessage(int id, string content)
        {
            using var conn = _dataSource.OpenConnection();
            var paramaters = new {id, content, timestamp = DateTimeOffset.Now};
            return conn.QueryFirst<Message>(@$"
            update chat_app.messages set content=@content, timestamp=@timestamp where id=@id
            Returning *;
            ");
        }

        public void DeleteMessage(int id)
        {
            using var conn = _dataSource.OpenConnection();
            
            conn.Execute(@$"
            delete from chat_app.messages where id=@id;
            ", id);
        }

        
       
    }
}