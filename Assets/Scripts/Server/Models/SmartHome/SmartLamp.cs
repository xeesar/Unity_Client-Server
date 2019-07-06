using UnityEngine;

namespace Server.Scripts.Models
{
    public class SmartLamp : SmartThing
    {
        [Header("Component")]
        [SerializeField] private Light _light;

        public override void HandleCommand()
        {
            _light.enabled = !_light.enabled;
        }
    }
}
