using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MarvelousGameJamProject.InGame
{
    public class TimerPresenter
    {
        private readonly Text _timerText;
        public Timer Timer { get; }

        public TimerPresenter(Timer timer, Text timerText)
        {
            Timer = timer;
            _timerText = timerText;
            // タイマーの状態が変わったときにUIを更新
            Timer.RemainingTime.Subscribe(UpdateTimerUI).AddTo(_timerText);
            Timer.IsRunning
                .Where(isRunning => !isRunning)
                .Subscribe(OnTimerEnd).AddTo(_timerText);
        }

        private void UpdateTimerUI(float remainingTime)
        {
            _timerText.text = $"{remainingTime:F0}";
        }

        private void OnTimerEnd(bool isRunning)
        {
            if (Timer.RemainingTime.Value <= 0)
            {

            }
        }

        public void StartTimer()
        {
            Timer.TimerStart().Forget();
        }

        public void StopTimer()
        {
            //Timer.TimerEnd();
        }

        public void ResetTimer()
        {
            // 必要に応じてリセット処理を追加
        }
    }
}