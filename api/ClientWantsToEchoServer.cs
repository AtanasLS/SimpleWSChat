using System.Text.Json;
using Fleck;
using lib;

namespace api
{
    public class ClientWantsToEchoServerDto : BaseDto
    {
        public string? messageContent { get; set; }
    }
    public class ClientWantsToEchoServer : BaseEventHandler<ClientWantsToEchoServerDto>
    {
        public override Task Handle(ClientWantsToEchoServerDto dto, IWebSocketConnection socket)
        {
            var echo = new ServerEchoClient()
            {
                echoValue = "echo:" +  dto.messageContent
            };

            var messageToClient = JsonSerializer.Serialize(echo);
           
            socket.Send(messageToClient);

            return Task.CompletedTask;
        }
    }

    public class ServerEchoClient : BaseDto
    {
        public string? echoValue { get; set; }
    }
}