using Server.Scripts.Data;
using Server.Scripts.Models;
using Common.Scripts.Structures;
using Common.Scripts.Enums;
using Common.Scripts.Observer;
using UnityEngine;

namespace Server.Scripts.Managers
{
    public class ServerManager : MonoBehaviour, IObserver
    {
        public ServerConfig config;

        public BaseServer Server { get; private set; }

        public void Start()
        {
            Server = new TCPServer(config);

            Server.StartServer();
            Server.AddObserver(this);
        }

        private void OnDisable()
        {
            Server.StopServer();
        }

        public void Update(object packet)
        {
            OnPacketRecieved((Packet)packet);
        }

        private void OnPacketRecieved(Packet packet)
        {
            switch(packet.CommandType)
            {
                case CommandType.SwitchLight:
                    break;
                case CommandType.Explode:
                    break;
                default:
                    break;
            }
        }
    }
}
