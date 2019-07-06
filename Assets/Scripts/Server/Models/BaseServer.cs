using Server.Scripts.Data;
using Common.Scripts.Structures;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

        protected Subject<Packet> _onPacketReceived = new Subject<Packet>();


        public IObservable<Packet> OnPacketReceivedAsObservable()
        {
            return _onPacketReceived ?? (_onPacketReceived = new Subject<Packet>());
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
            _socket.Listen(Config.maxPendingConnections);

            Observable.Start(() => ListenAny())
                .AsUnitObservable()
                .Subscribe();

            Debug.LogAssertion($"[{_endPoint.Address}] Started...");
        }

        public virtual void StopServer()
        {
            _isServerActive = false;

            _socket.Close();

            Debug.LogAssertion($"[{_endPoint.Address}] Closed...");
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
                Packet packet = GetPacket(listener);

                _onPacketReceived.OnNext(packet);
                _onPacketReceived.OnCompleted();

                listener.Send(Encoding.UTF8.GetBytes("Success."));
            }
            catch
            {
                listener.Send(Encoding.UTF8.GetBytes("Packet is broken."));
            }
            finally
            {             
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        private Packet GetPacket(Socket listener)
        {
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();

            do
            {
                size = listener.Receive(buffer);
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (listener.Available > 0);

            return JsonUtility.FromJson<Packet>(data.ToString());
        }

        #endregion
    }
}