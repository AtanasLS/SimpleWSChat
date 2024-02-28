using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Externalities.Repositories;
using Fleck;
using lib;

namespace api
{

    public class ClientWantsToSignInDto : BaseDto
    {
        //public int userId { get; set; }
        public string? username { get; set; }
    }
    public class ClientWantsToSignIn(UserRepository userRepository) : BaseEventHandler<ClientWantsToSignInDto>
    {
        public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
        {
            StateService.Connections[socket.ConnectionInfo.Id].Username = dto.username;
            var messageToClient = new ServerAddsClientToRoom(){
                message = "User with username: " + dto.username,
                username = dto.username
            };

            var user = userRepository.FindUserByUsername(dto.username!);

            if(user == null)
            userRepository.CreateUser(dto.username!);

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