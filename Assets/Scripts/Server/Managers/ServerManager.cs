using System;
using Server.Scripts.Data;
using Server.Scripts.Models;
using Common.Scripts.Structures;
using Common.Scripts.Command;
using UnityEngine;
using UniRx;

namespace Server.Scripts.Managers
{
    public class ServerManager : Singleton<ServerManager>
    {
        [Header("Options")]
        [SerializeField] private ServerConfig _config;

        public BaseServer Server { get; private set; }

        private Subject<CommandType> _onExecuteCommand = new Subject<CommandType>();

        public void Start()
        {
            Server = new TCPServer(_config);

            Server.OnPacketReceivedAsObservable().Subscribe(packet => OnPacketReceived(packet));
            Server.StartServer();
        }

        public IObservable<CommandType> OnExecuteCommandAsObservable()
        {
            return _onExecuteCommand ?? (_onExecuteCommand = new Subject<CommandType>());
        }

        private void OnDisable()
        {
            Server?.StopServer();
        }

        private void OnPacketReceived(Packet packet)
        {
            _onExecuteCommand.OnNext(packet.CommandType);
        }
    }
}
