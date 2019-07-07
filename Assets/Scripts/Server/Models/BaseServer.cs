using Common.Scripts.Data;
using Common.Scripts.Structures;
using Common.Scripts.Enums;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace Server.Scripts.Models
{
    public abstract class BaseServer
    {
        public ServerConfig Config { get; private set; }

        protected IPEndPoint _endPoint;
        protected Socket _socket;

        protected bool _isServerActive;

        protected Subject<CommandPacket> _onPacketReceived = new Subject<CommandPacket>();


        public IObservable<CommandPacket> OnPacketReceivedAsObservable()
        {
            return _onPacketReceived ?? (_onPacketReceived = new Subject<CommandPacket>());
        }

        #region Server Logic
        public BaseServer(ServerConfig config)
        {
            Config = config;

            _endPoint = new IPEndPoint(IPAddress.Parse(config.serverIP), config.serverPort);
        }

        public virtual void StartServer()
        {
            _isServerActive = true;

            _socket.Bind(_endPoint);

            StartListening();

            Debug.LogAssertion($"[{_endPoint.Address}] Started...");
        }

        public virtual void StopServer()
        {
            _isServerActive = false;

            _socket.Close();

            Debug.LogAssertion($"[{_endPoint.Address}] Closed...");
        }

        private async void StartListening()
        {
            _socket.Listen(Config.maxPendingConnections);

            await Task.Run(() => ListenAny());
        }

        private void ListenAny()
        {
            while (_isServerActive)
            {
                var listener = _socket.Accept();
                OnConnectionCreated(listener);
            }
        }

        private void OnConnectionCreated(Socket listener)
        {
            try
            {
                CommandPacket packet = GetPacket(listener);
                _onPacketReceived.OnNext(packet);

                SendAnswer(listener, AnswerType.Success);
            }
            catch
            {
                SendAnswer(listener, AnswerType.Failed);
            }
            finally
            {
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        private CommandPacket GetPacket(Socket listener)
        {
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();

            do
            {
                size = listener.Receive(buffer);
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (listener.Available > 0);

            return JsonUtility.FromJson<CommandPacket>(data.ToString());
        }

        private void SendAnswer(Socket listener, AnswerType answerType)
        {
            AnswerPacket packet = new AnswerPacket { AnswerType = answerType };
            string packetJSON = JsonUtility.ToJson(packet);

            listener.Send(Encoding.UTF8.GetBytes(packetJSON));
        }

        #endregion
    }
}