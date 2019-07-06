using Common.Scripts.Command;
using UnityEngine;

namespace Server.Scripts.Models
{
    public abstract class SmartThing : MonoBehaviour, IReceiver
    {
        public abstract void HandleCommand();
    }
}
