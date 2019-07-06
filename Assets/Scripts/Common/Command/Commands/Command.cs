namespace Common.Scripts.Command
{
    public interface ICommand
    {
        CommandType CommandType { get; }

        void Execute();
        void Undo();
    }
}
