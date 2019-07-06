using System.Collections.Generic;
using Client.Scripts.Managers;
using Client.Scripts.Models;
using Common.Scripts.Models;
using Common.Scripts.Enums;
using Common.Scripts.Command;
using Common.Scripts.Extensions;
using UnityEngine;
using UniRx;

namespace Client.Scripts.Controllers
{
    public class SmartHomeController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private List<CommandData> _commands;

        private ControlPanel _controlPanel;

        private void Start()
        {
            _controlPanel = new ControlPanel();

            InitializeSmartButtons();
        }

        private void InitializeSmartButtons()
        {
            SmartHomeButton smartHomeButton;

            for(int i = 0; i < _commands.Count; i++)
            {
                smartHomeButton = _commands[i].receiver as SmartHomeButton;
                smartHomeButton.CommandType = _commands[i].commandType;
                smartHomeButton.OnSmartButtonClickAsObservable().Subscribe(commandType => OnCommandSend(commandType));

                _controlPanel.BindCommand(CommandFactory.CreateCommand(_commands[i]));
            }
        }

        private void OnCommandSend(CommandType commandType)
        {
            ClientManager.Instance.SendCommandRequest(commandType).ObserveOnMainThread().Subscribe(result => {
                _controlPanel.Invoke(commandType);
            },
            exception => Debug.LogError(exception));

        }
    }
}
