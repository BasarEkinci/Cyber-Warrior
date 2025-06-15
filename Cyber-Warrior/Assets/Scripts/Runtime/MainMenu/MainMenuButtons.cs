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

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
