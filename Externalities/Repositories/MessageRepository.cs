using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
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