using Client.Scripts.Enums;
using Common.Scripts.Enums;

namespace Client.Scripts.Structures
{
    [System.Serializable]
    public struct SmartButtonData
    {
        public CommandType commandType;
        public SmartButtonStatus status;
    }
}
