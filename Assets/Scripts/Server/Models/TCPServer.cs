using Common.Scripts.Data;
using System.Net.Sockets;

namespace Server.Scripts.Models
{
    public class TCPServer : BaseServer
    {
        public TCPServer(ServerConfig config) : base(config)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}