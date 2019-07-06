using Common.Scripts.Command;
using Common.Scripts.Enums;

namespace Common.Scripts.Extensions
{
    public class CommandFactory
    {
        public static ICommand CreateCommand(CommandData commandData)
        {
            return GetCommand(commandData);
        }

        private static ICommand GetCommand(CommandData commandData)
        {
            switch(commandData.commandType)
            {
                case CommandType.SwitchLight:
                    return new LightCommand(commandData.receiver);
                case CommandType.SwitchAudio:
                    return new SwitchAudioCommand(commandData.receiver);
                default:
                    return new LightCommand(commandData.receiver);
            }
        }
    }
}
