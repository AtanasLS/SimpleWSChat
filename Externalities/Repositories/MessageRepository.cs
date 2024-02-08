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
            room_id as {nameof(Message.roomId)}
            FROM chat_app.messages
            join chat_app.users on chat_app.messages = chat_app.users.id
            join chat_app.rooms on chat_app.messages = chat_app.rooms.id;
            ");
            //TODO: This is wrong fix it!!!
        }

        public Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public Message CreateMessage(string content, DateTimeOffset timestamp, int userId, int roomId)
        {
            throw new NotImplementedException();
        }

        public Message UpdateMessage(int id, string content)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        
       
    }
}