using UnityEngine;

namespace Server.Scripts.Models
{
    public class SmartAudio : SmartThing
    {
        [Header("Components")]
        [SerializeField] private AudioSource _audioSource;

        public bool IsPaused => !_audioSource.isPlaying;

        public override void HandleCommand()
        {
            if(IsPaused)
            {
                _audioSource.UnPause();
            }
            else
            {
                _audioSource.Pause();
            }


        }
    }
}
