using Client.Scripts.Models;
using Common.Scripts.Data;
using Common.Scripts.Structures;
using Common.Scripts.Enums;
using System;
using UnityEngine;
using UniRx;

namespace Client.Scripts.Managers
{
    public class ClientManager : Singleton<ClientManager>
    {
        [Header("Options")]
        [SerializeField] private ServerConfig _serverConfig;

        private BaseClient _client;

        public void Start()
        {
            _client = new TCPClient(_serverConfig);
        }

        public IObservable<AnswerPacket> SendCommandRequest(CommandType commandType)
        {
            CommandPacket commandPacket = new CommandPacket { CommandType = commandType};

            return _client.CommandRequest(commandPacket);
        }
    }
}
