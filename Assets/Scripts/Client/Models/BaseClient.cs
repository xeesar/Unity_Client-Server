using Common.Scripts.Data;
using Common.Scripts.Structures;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UniRx;

namespace Client.Scripts.Models
{
    public abstract class BaseClient
    {
        public ServerConfig Config { get; private set; }

        protected IPEndPoint _endPoint;

        public BaseClient(ServerConfig config)
        {
            Config = config;

            _endPoint = new IPEndPoint(IPAddress.Parse(config.serverIP), config.serverPort);
        }

        public IObservable<AnswerPacket> SendCommandToServer(CommandPacket packet)
        {
            return Observable.Start(() => SendCommand(packet));
        }


        private AnswerPacket SendCommand(CommandPacket packet)
        {
            string dataJSON = JsonUtility.ToJson(packet);
            var data = Encoding.UTF8.GetBytes(dataJSON);

            Socket connection = CreateConnection();
            connection.Send(data);

            var answer = GetAnswer(connection);

            CloseConnection(connection);

            return answer;
        }

        protected abstract Socket CreateConnection();

        protected virtual void CloseConnection(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }


        private AnswerPacket GetAnswer(Socket socket)
        {
            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = socket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (socket.Available > 0);

            AnswerPacket answerPacket = JsonUtility.FromJson<AnswerPacket>(answer.ToString());

            return answerPacket;
        }
    }
}