using Common.Scripts.Enums;

namespace Common.Scripts.Command
{
    public class SwitchAudioCommand : ICommand
    {
        public CommandType CommandType => CommandType.SwitchAudio;

        private Receiver _receiver;

        public SwitchAudioCommand(Receiver receiver)
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