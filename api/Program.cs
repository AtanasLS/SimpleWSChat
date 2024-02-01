using System.Reflection;
using Fleck;
using lib;

var builder = WebApplication.CreateBuilder(args);

var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

var app = builder.Build();

var server = new WebSocketServer("ws://0.0.0.0:8181");


var wsConnections = new List<IWebSocketConnection>();

server.Start(socket =>
{

    socket.OnOpen = () => 
    {
        wsConnections.Add(socket);
    };

    socket.OnMessage = message => 
    {   
        try
        {
            app.InvokeClientEventHandler(clientEventHandlers, socket, message);
        }
        catch(Exception e)
        {
            throw e = new Exception("It couldn't send the message!");
        }
    };
});



WebApplication.CreateBuilder(args).Build().Run();
