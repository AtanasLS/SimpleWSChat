using Fleck;

var server = new WebSocketServer("ws://0.0.0.0:8181");


var name1 = "Atanas";

var name2 = "Bob";

var wsConnections = new List<IWebSocketConnection>();
var users = new List<string>
{
    name1,
    name2
};

server.Start(socket =>
{

    socket.OnOpen = () => 
    {
        wsConnections.Add(socket);
    };
    socket.OnClose = () => Console.WriteLine("Close!");

    
    socket.OnMessage = message => 
    {
        foreach (var webSocketConnection in wsConnections)
        {
            /*
            if(message.ToLower().Contains(name1.ToLower()) ||
                message.ToLower().Contains(name2.ToLower()))
            {
                webSocketConnection.Send(message);
            }
            else
            {
                webSocketConnection.Send("There is no such user!");
            }   
            */

            webSocketConnection.Send(message);
        }
    };
});



WebApplication.CreateBuilder(args).Build().Run();
