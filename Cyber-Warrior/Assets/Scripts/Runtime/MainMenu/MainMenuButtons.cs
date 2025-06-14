using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void Play(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
