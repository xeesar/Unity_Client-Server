using Common.Scripts.Data;
using System.Net.Sockets;

namespace Client.Scripts.Models
{
    public class TCPClient : BaseClient
    {

        public TCPClient(ServerConfig config) : base(config)
        {

        }

        protected override Socket CreateConnection()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(_endPoint);
            return socket;
        }
    }
}
