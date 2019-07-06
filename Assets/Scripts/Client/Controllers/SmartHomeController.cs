using Client.Scripts.View;
using Client.Scripts.Managers;
using Common.Scripts.Enums;
using UnityEngine;
using UniRx;

namespace Client.Scripts.Controllers
{
    public class SmartHomeController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SmartHomePanelView _view;

        public void Start()
        {
            //_view.LightButton.OnClickAsObservable()
            //    .Subscribe(x => OnCommandSend(CommandType.SwitchLight));

            //_view.AudioButton.OnClickAsObservable()
            //    .Subscribe(x => OnCommandSend(CommandType.SwitchAudio));
        }

        private void OnCommandSend(CommandType commandType)
        {
            ClientManager.Instance.SendCommandRequest(commandType).Subscribe(
            result => {
                Debug.Log(result.AnswerType);
            },
            exception => Debug.LogError(exception));

        }
    }
}
