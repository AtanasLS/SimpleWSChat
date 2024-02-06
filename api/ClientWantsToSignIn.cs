using System;
using System.Collections.Generic;
using System.Linq;
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
            return Task.CompletedTask;
        }
    }
}