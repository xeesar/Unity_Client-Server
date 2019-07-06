using Client.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.View
{
    public class SmartHomeButtonView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _smartElementStatusImage;

        [Header("Options")]
        [SerializeField] private Color _smartElementActiveColor;
        [SerializeField] private Color _smartElementInactiveColor;

        public void DisplayStatus(SmartButtonStatus status)
        {
            _smartElementStatusImage.color = status == SmartButtonStatus.Active ? _smartElementActiveColor : _smartElementInactiveColor;
        }
    }
}
