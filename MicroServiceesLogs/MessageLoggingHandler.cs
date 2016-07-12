using System.Text;
using System.Threading.Tasks;

namespace MicroServiceLogs
{
    
    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            string logmessage = string.Format("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
            await Task.Run(() =>
                new LogWriter(logmessage).LogWrite(logmessage));
        }


        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            string logmessage = string.Format("{0} - Response: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
            await Task.Run(() =>
                new LogWriter(logmessage).LogWrite(logmessage));
        }
    }

    
}
