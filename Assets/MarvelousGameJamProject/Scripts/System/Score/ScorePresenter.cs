using HikanyanLaboratory.System;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarvelousGameJamProject.System
{
    public class ScorePresenter
    {
        private readonly Score _score;
        private readonly Text _scoreText;

        public int ScoreValue { get { return _score.ScoreValue.Value; } }

        public ScorePresenter(Score score, Text scoreText)
        {
            _score = score;
            _scoreText = scoreText;

            // スコアが更新されたときにUIを更新
            _score.ScoreValue.Subscribe(UpdateScoreUI).AddTo(_scoreText);
        }

        private void UpdateScoreUI(int score)
        {
            _scoreText.text = $"{score}杯";
        }

        public void AddScore(int score)
        {
            _score.AddScore(score);
        }

        public void ResetScore()
        {
            _score.ResetScore();
        }
    }
}