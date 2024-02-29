
using System.ComponentModel.DataAnnotations;
using Externalities.Repositories;
using Fleck;
using lib;

namespace api
{
    public class ClientsWantsToRegisterDto : BaseDto
    {
        public string? Username { get; set; }
    }
    public class ClientsWantsToRegister(UserRepository userRepository) : BaseEventHandler<ClientsWantsToRegisterDto>
    {
        public override Task Handle(ClientsWantsToRegisterDto dto, IWebSocketConnection socket)
        {
            //if(userRepository.FindUserByUsername(dto.Username!).username == dto.Username)
            //  throw new ValidationException("User with that username already exsits.");
            
            var user = userRepository.CreateUser(dto.Username!);

            StateService.Connections[socket.ConnectionInfo.Id].currentUser = user;

            return Task.CompletedTask;
        }
    }
}