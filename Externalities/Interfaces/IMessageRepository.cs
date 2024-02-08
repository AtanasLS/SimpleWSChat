using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Externalities.QueryModels;

namespace Externalities.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAllMessages();
        Message GetMessageById(int id);
        Message CreateMessage(string content, DateTimeOffset timestamp, int userId, int roomId);
        Message UpdateMessage(int id, string content);
        void DeleteMessage(int id);
    }
}