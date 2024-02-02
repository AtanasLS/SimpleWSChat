using System.Reflection;
using api;
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
        WebSocketConnections.wsConnections.Add(socket);
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




Console.ReadLine();

public class WebSocketConnections
{
    public static List<IWebSocketConnection> wsConnections = new List<IWebSocketConnection>();
    // class members
}


