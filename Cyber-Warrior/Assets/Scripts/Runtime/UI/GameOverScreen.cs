using DG.Tweening;
using Runtime.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameStatDataHolder statDataHolder;
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text totalKillsText;
        [SerializeField] private TMP_Text collectedScrapText;
        [SerializeField] private Button restartButton;

        private Sequence _sequence;

        private void OnEnable()
        {
            totalKillsText.text = $"Total Kills\n0";
            collectedScrapText.text = $"Collected Scrap\n0";

            _sequence = DOTween.Sequence();
            _sequence.SetEase(Ease.OutBack);

            _sequence.Append(header.transform.DOScale(Vector3.zero, 1f).From());
            _sequence.Append(totalKillsText.transform.DOScale(Vector3.zero, 1f).From().OnComplete(() =>
            {
                AnimateTextNumber(totalKillsText, "Total Kills\n", statDataHolder.TotalKills);
            }));
            _sequence.Append(collectedScrapText.transform.DOScale(Vector3.zero, 1f).From().OnComplete(() =>
            {
                AnimateTextNumber(collectedScrapText, "Collected Scrap\n", statDataHolder.CollectedScrap);
            }));
            _sequence.Append(restartButton.transform.DOScale(Vector3.zero, 1f).From());

            _sequence.OnComplete(() => Debug.Log("Sequence Completed"));
        }

        private void AnimateTextNumber(TMP_Text textComponent, string prefix, int targetValue, float duration = 1f)
        {
            int currentValue = 0;
            DOTween.To(() => currentValue, x =>
                {
                    currentValue = x;
                    textComponent.text = $"{prefix}{currentValue}";
                },
                targetValue,
                duration
            ).SetEase(Ease.OutCubic);
        }
    }
}
