using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fleck;
using lib;

namespace api
{
    public class ClientWantsToEnterRoomDto : BaseDto
    {
        public int roomId { get; set; }
    }
    public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
    {
        public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
        {
           var isSuccess = StateService.AddToRoom(socket, dto.roomId);
            socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom{
                message = "You were successfully added to room with ID: " + dto.roomId
            }));
            return Task.CompletedTask;
        }
    }

    public class ServerAddsClientToRoom : BaseDto
    {
        public string message { get; set; }
    }
}