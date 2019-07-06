using System.Collections.Generic;
using Common.Scripts.Command;

namespace Server.Scripts.Models
{
    public class ControlPanel
    {
        private Dictionary<CommandType, ICommand> _commands;

        public void AddCommand(ICommand command)
        {
            if(_commands == null)
            {
                _commands = new Dictionary<CommandType, ICommand>();
            }

            if (_commands.ContainsKey(command.CommandType)) return;

            _commands.Add(command.CommandType, command);
        }

        public void Invoke(CommandType commandType)
        {
            if(_commands.ContainsKey(commandType))
            {
                _commands[commandType].Execute();
            }
        }
    }
}
