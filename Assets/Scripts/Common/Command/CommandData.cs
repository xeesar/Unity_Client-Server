using Common.Scripts.Enums;

namespace Common.Scripts.Command
{
    [System.Serializable]
    public struct CommandData
    {
        public CommandType commandType;
        public Receiver receiver;
    }
}
