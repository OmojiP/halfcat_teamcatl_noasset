using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.System
{
    public class Score
    {
        private IntReactiveProperty _score = new IntReactiveProperty(0);

        public IReadOnlyReactiveProperty<int> ScoreValue => _score;

        public void AddScore(int score)
        {
            _score.Value += score;
        }

        public void ResetScore()
        {
            _score.Value = 0;
        }
    }
}