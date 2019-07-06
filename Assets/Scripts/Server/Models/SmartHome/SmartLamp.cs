using UnityEngine;

namespace Server.Scripts.Models
{
    public class SmartLamp : SmartThing
    {
        [Header("Component")]
        [SerializeField] private Light _light;

        private bool _isEnabled = true;

        public override void HandleCommand()
        {
            _isEnabled = !_isEnabled;
            _light.enabled = _isEnabled;
        }
    }
}
