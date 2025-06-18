using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private GameStatDataHolder statDataHolder;
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text totalKillsText;
        [SerializeField] private TMP_Text collectedScrapText;
        [SerializeField] private Button restartButton;
        
        private Sequence _sequence;
        private void OnEnable()
        {
            playerDeathEvent.OnEventRaised += OnPlayerDeath;
            totalKillsText.text = $"Total Kills\n{statDataHolder.TotalKills}";
            collectedScrapText.text = $"Collected Scrap\n{statDataHolder.CollectedScrap}";
            _sequence = DOTween.Sequence();
            _sequence.SetEase(Ease.OutBack);
            _sequence.Append(header.transform.DOScale(Vector3.one, 1f));
            _sequence.Append(totalKillsText.transform.DOScale(Vector3.one, 1f));
            _sequence.Append(collectedScrapText.transform.DOScale(Vector3.one, 1f));
            _sequence.Append(restartButton.transform.DOScale(Vector3.one, 1f));
            _sequence.OnComplete(()=> Debug.Log("Game Over"));
        }

        private void OnPlayerDeath()
        {

        }
        private void OnDisable()
        {
            header.gameObject.transform.DOScale(Vector3.zero, 0.2f);
            totalKillsText.gameObject.transform.DOScale(Vector3.zero, 0.2f);
            collectedScrapText.gameObject.transform.DOScale(Vector3.zero, 0.2f);
            restartButton.gameObject.transform.DOScale(Vector3.zero, 0.2f);
            Debug.Log("Game Over Screen Disabled");
            playerDeathEvent.OnEventRaised -= OnPlayerDeath;
        }
    }
}
