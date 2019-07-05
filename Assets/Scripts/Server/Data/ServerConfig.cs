using UnityEngine;

namespace Server.Scripts.Data
{
    [CreateAssetMenu(fileName = "ServerConfig", menuName = "Configs/Server Config")]
    public class ServerConfig : ScriptableObject
    {
        [Header("Server Options")]
        public string serverIP;
        public int serverPort;
        public int maxPendingConnections;
    }
}