using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using lib;

namespace api
{
    public class ClientWantsToBroadcastToRoomDto : BaseDto
    {
        public string? message { get; set; }
        public int roomId { get; set; }
    }
    public class ClientWantsToBroadcastToRoom : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
    {
        public override Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
        {
            StateService.BroadCastToRoom(dto.roomId, dto.message);
            return Task.CompletedTask;
        }
    }
}