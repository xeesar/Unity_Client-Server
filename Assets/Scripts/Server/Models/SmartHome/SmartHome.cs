using Server.Scripts.Managers;
using Common.Scripts.Command;
using Common.Scripts.Extensions;
using Common.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Server.Scripts.Models
{
    public class SmartHome : MonoBehaviour
    {
        [Header("Commands")]
        [SerializeField] private List<CommandData> _commands;

        private ControlPanel _controlPanel;

        private void Start()
        {
            InitializeSmartHome();
        }

        private void InitializeSmartHome()
        {
            _controlPanel = new ControlPanel();

            SetupSmartThings();

            ServerManager.Instance.OnExecuteCommandAsObservable()
                .ObserveOnMainThread()
                .Subscribe(commandType => _controlPanel.Invoke(commandType));
        }

        private void SetupSmartThings()
        {
            for(int i = 0; i < _commands.Count; i++)
            {
                _controlPanel.BindCommand(CommandFactory.CreateCommand(_commands[i]));
            }
        }
    }
}
