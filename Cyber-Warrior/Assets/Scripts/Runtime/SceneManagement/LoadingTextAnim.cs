using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Runtime.SceneManagement
{
    public class LoadingTextAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadingText;
        
        [SerializeField] private string baseText = "Loading";
        [SerializeField] private float animationDelay = 0.5f;
        [SerializeField] private int maxDots = 3;
        
        private CancellationTokenSource _cts;
        
        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            TextAnimation().Forget();
        }
        
        private void OnDisable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid TextAnimation()
        {
            int dotCount = 0;
            
            while (!_cts.Token.IsCancellationRequested)
            {
                string dots = new string('.', dotCount);
                loadingText.text = $"{baseText}{dots}";
                dotCount = (dotCount + 1) % (maxDots + 1);
                await UniTask.Delay((int)(animationDelay * 1000), cancellationToken: _cts.Token);
            }
        }
    }
}
