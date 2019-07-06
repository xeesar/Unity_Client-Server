using Common.Scripts.Enums;

namespace Common.Scripts.Command
{
    public class LightCommand : ICommand
    {
        public CommandType CommandType => CommandType.SwitchLight;

        private Receiver _receiver;

        public LightCommand(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.HandleCommand();
        }

        public void Undo()
        {

        }
    }
}
