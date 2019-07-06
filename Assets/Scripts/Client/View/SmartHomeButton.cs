using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Client.Scripts.View
{
    public class SmartHomeButton : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _smartElementStatusImage;
        [SerializeField] private Button _smartButton;

        [Header("Options")]
        [SerializeField] private Color _smartElementActiveColor;
        [SerializeField] private Color _smartElementInactiveColor;

        public void Start()
        {
            _smartButton.OnClickAsObservable().Subscribe(x => );
        }
    }
}
