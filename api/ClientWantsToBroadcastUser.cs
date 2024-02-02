using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fleck;
using lib;

namespace api
{
    public class ClientWantsToBroadcastUserDto : BaseDto 
    {
        public string? username { get; set; }
    }
    public class ClientWantsToBroadcastUser : BaseEventHandler<ClientWantsToBroadcastUserDto>
    {
        public override Task Handle(ClientWantsToBroadcastUserDto dto, IWebSocketConnection socket)
        {

            
            var username = dto.username;
            if(username.Equals("Test"))
            {
                var message = new ServerBroadcastsUser() 
                {
                    message = "succsess",
                };
                
                foreach( var webConnection in WebSocketConnections.wsConnections ){

                var messageToClient = JsonSerializer.Serialize(message);
                
                webConnection.Send(messageToClient);
                }
                return Task.CompletedTask;
            }
            else
            {
                var message = new ServerBroadcastsUser 
                {
                    message = "unsuccsessful!"
                };

                var messageToClient = JsonSerializer.Serialize(message);
                
                socket.Send(messageToClient);
                
                return Task.CompletedTask;
            }
            
            
        }
    }

    public class ServerBroadcastsUser : BaseDto 
    {
        public string? message { get; set; }
    }
}