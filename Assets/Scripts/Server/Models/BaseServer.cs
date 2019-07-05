using Server.Scripts.Data;
using Common.Scripts.Observer;
using Common.Scripts.Structures;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Server.Scripts.Models
{
    public abstract class BaseServer : IObservable
    {
        public ServerConfig Config { get; private set; }

        protected IPEndPoint _endPoint;
        protected Socket _socket;

        protected bool _isServerActive;

        private List<IObserver> _observers;

        #region Server Logic
        public BaseServer(ServerConfig config)
        {
            _observers = new List<IObserver>();

            Config = config;

            _endPoint = new IPEndPoint(IPAddress.Parse(config.serverIP), config.serverPort);
        }

        public virtual void StartServer()
        {
            _isServerActive = true;

            _socket.Bind(_endPoint);
            _socket.Listen(Config.maxPendingConnections);

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
            await Task.Run(() => ListenAny());
        }

        private void ListenAny()
        {
            while (_isServerActive)
            {
                var listener = _socket.Accept();

                Packet packet = GetPacket(listener);
                NotifyObservers(packet);

                listener.Send(Encoding.UTF8.GetBytes("Success."));
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
        #region Observer Realization
        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(object packet)
        {
            for(int i = 0; i < _observers.Count; i++)
            {
                _observers[i].Update(packet);
            }
        }
        #endregion
    }
}