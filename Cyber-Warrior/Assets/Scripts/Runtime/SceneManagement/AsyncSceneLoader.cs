using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.SceneManagement
{
    public class AsyncSceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider loadingSlider;

        private void Start()
        {
            if(loadingScreen.activeSelf)
                loadingScreen.SetActive(false);
        }

        public void LoadLevelButton(int sceneIndex)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadLevelAsync(sceneIndex));
        }

        IEnumerator LoadLevelAsync(int sceneIndex)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                loadingSlider.value = progress;
                yield return null;
            }
            loadingScreen.SetActive(false);
        }
    }
}
