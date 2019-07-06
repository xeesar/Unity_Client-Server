using System;
using Client.Scripts.Structures;
using Client.Scripts.View;
using Client.Scripts.Enums;
using Common.Scripts.Enums;
using Common.Scripts.Command;
using UnityEngine;
using UniRx;

namespace Client.Scripts.Models
{
    public class SmartHomeButton : Receiver
    {
        [Header("Components")]
        [SerializeField] private SmartHomeButtonView _view;

        private SmartButtonData _smartButtonData;

        public CommandType CommandType
        {
            get => _smartButtonData.commandType;
            set => _smartButtonData.commandType = value;
        }

        private Subject<CommandType> _onSmartButtonClick = new Subject<CommandType>();

        public IObservable<CommandType> OnSmartButtonClickAsObservable()
        {
            return _onSmartButtonClick ?? (_onSmartButtonClick = new Subject<CommandType>());
        }

        public override void HandleCommand()
        {
            SwitchButtonStatus();
            _view.DisplayStatus(_smartButtonData.status);
        }

        public void SmartButtonClick()
        {
            _onSmartButtonClick.OnNext(CommandType);
        }

        private void SwitchButtonStatus()
        {
            _smartButtonData.status = _smartButtonData.status == SmartButtonStatus.Active 
                ? SmartButtonStatus.InActive 
                : SmartButtonStatus.Active;
        }
    }
}
