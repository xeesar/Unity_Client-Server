using UnityEngine;
using SceneLoader = UnityEngine.SceneManagement.SceneManager;

namespace Lobby.Scripts.Managers
{
    public class SceneManager : MonoBehaviour
    {
        private void Awake()
        {
            Screen.fullScreen = false;
        }

        public void LoadScene(int sceneIndex)
        {
            SceneLoader.LoadSceneAsync(sceneIndex);
        }
    }
}
