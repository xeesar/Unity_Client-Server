using Server.Scripts.Managers;
using Common.Scripts.Command;
using UnityEngine;
using UniRx;

namespace Server.Scripts.Models
{
    public class SmartHome : MonoBehaviour
    {
        [Header("Smart Things")]
        [SerializeField] private SmartLamp _smartLamp;
        [SerializeField] private SmartAudio _smartAudio;

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

        //TODO - Большое полотно кода, когда много команд.
        private void SetupSmartThings()
        {
            _controlPanel.AddCommand(new LightCommand(_smartLamp));
            _controlPanel.AddCommand(new SwitchAudioCommand(_smartAudio));
        }
    }
}
