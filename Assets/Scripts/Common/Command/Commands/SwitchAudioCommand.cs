namespace Common.Scripts.Command
{
    public class SwitchAudioCommand : ICommand
    {
        public CommandType CommandType => CommandType.SwitchAudio;

        private IReceiver _receiver;

        public SwitchAudioCommand(IReceiver receiver)
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