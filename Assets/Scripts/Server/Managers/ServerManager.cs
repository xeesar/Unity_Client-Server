using System;
using Common.Scripts.Data;
using Server.Scripts.Models;
using Common.Scripts.Structures;
using Common.Scripts.Enums;
using UnityEngine;
using UniRx;

namespace Server.Scripts.Managers
{
    public class ServerManager : Singleton<ServerManager>
    {
        [Header("Options")]
        [SerializeField] private ServerConfig _serverConfig;

        public BaseServer Server { get; private set; }

        private Subject<CommandType> _onCommandReceived = new Subject<CommandType>();

        public void Start()
        {
            Server = new TCPServer(_serverConfig);

            Server.StartServer();
            Server.OnPacketReceivedAsObservable().Subscribe(packet => OnPacketReceived(packet));
        }

        public IObservable<CommandType> OnCommandReceivedAsObservable()
        {
            return _onCommandReceived ?? (_onCommandReceived = new Subject<CommandType>());
        }

        private void OnDisable()
        {
            Server?.StopServer();
        }

        private void OnPacketReceived(CommandPacket packet)
        {
            _onCommandReceived.OnNext(packet.CommandType);
        }
    }
}
