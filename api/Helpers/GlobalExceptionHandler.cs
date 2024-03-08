using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public class GlobalExceptionHandler
    {
    public static void Handle(this Exception exception, IWebSocketConnection socket)
    {
        Log.Error(exception, "Global exception handler");

        switch (exception)
        {
            case ValidationException validationException:
                socket.SendError($"Validation error: {validationException.Message}");
                break;
            case AuthenticationException authenticationException:
                socket.SendError($"Authentication error: {authenticationException.Message}");
                break;
            case BadWordException badWordException:
                socket.SendError("Message contains bad word");
                break;
            default:
                socket.SendError(exception.Message);
                break;
        }
    }
    }
}