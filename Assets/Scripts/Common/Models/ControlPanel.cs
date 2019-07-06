using System.Collections.Generic;
using Common.Scripts.Command;
using Common.Scripts.Enums;

namespace Common.Scripts.Models
{
    public class ControlPanel
    {
        private Dictionary<CommandType, List<ICommand>> _commands;

        public void BindCommand(ICommand command)
        {
            if(_commands == null)
            {
                _commands = new Dictionary<CommandType, List<ICommand>>();
            }

            CommandType commandType = command.CommandType;

            if (!_commands.ContainsKey(commandType))
            {
                _commands.Add(commandType, new List<ICommand>());
            }

            _commands[commandType].Add(command);
        }

        public void Invoke(CommandType commandType)
        {
            if (!_commands.ContainsKey(commandType)) return;

            List<ICommand> _receivers = _commands[commandType];

            for(int i = 0; i < _receivers.Count; i++)
            {
                _receivers[i].Execute();
            }
        }
    }
}
