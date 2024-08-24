using HikanyanLaboratory.System;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText;

        private void Start()
        {
            _score.ScoreValue.Subscribe(score => _scoreText.text = $"Score: {score}").AddTo(this);
        }
    }
}