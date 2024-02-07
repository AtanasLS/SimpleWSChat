using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fleck;
using lib;

namespace api
{

    public class ClientWantsToSignInDto : BaseDto
    {
        public string? username { get; set; }
    }
    public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
    {
        public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
        {
            StateService.Connections[socket.ConnectionInfo.Id].Username = dto.username;
            var messageToClient = new ServerAddsClientToRoom(){
                message = "User with username: " + dto.username,
                username = dto.username
            };
            socket.Send(JsonSerializer.Serialize(messageToClient));
            return Task.CompletedTask;
        }
    }

    public class ServerAddsUserToClient() : BaseDto
    {
        public string? message { get; set; }
        public string? username { get; set; }
    }
}