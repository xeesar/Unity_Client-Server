using UnityEngine;

namespace Common.Scripts.Command
{
    public abstract class Receiver : MonoBehaviour
    {
        public abstract void HandleCommand();
    }
}
