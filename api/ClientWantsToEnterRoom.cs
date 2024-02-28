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
    public class ClientWantsToEnterRoomDto : BaseDto
    {
        public int roomId { get; set; }
    }
    public class ClientWantsToEnterRoom(RoomRepository roomRepository) : BaseEventHandler<ClientWantsToEnterRoomDto>
    {
        public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
        {   

            StateService.RemoveFromRoom(socket);

            var isSuccess = StateService.AddToRoom(socket, dto.roomId);

            var room = roomRepository.GetRoomById(dto.roomId);

            if(room == null)
            roomRepository.CreateRoom(dto.roomId ,"testRoom");

            socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom{
                message = "You were successfully added to room with ID: " + dto.roomId
            }));

            return Task.CompletedTask;
        }
    }

    public class ServerAddsClientToRoom : BaseDto
    {
        internal string? username;

        public string? message { get; set; }
    }
}