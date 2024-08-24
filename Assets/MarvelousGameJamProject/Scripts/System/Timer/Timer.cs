using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace HikanyanLaboratory.System
{
    public class Timer
    {
        private readonly FloatReactiveProperty _remainingTime;
        private float _duration;
        private UnityAction _onTimerEnd;
        public float InitialTime { get; private set; }  // 初期時間を保持
        public IReadOnlyReactiveProperty<float> RemainingTime => _remainingTime;
        public BoolReactiveProperty IsRunning { get; } = new BoolReactiveProperty(false);

        public Timer(float duration, UnityAction onTimerEnd)
        {
            _duration = duration;
            _onTimerEnd = onTimerEnd;
            InitialTime = duration;
            _remainingTime = new FloatReactiveProperty(duration);
        }

        public async UniTaskVoid TimerStart()
        {
            IsRunning.Value = true;
            _remainingTime.Value = _duration;

            while (_remainingTime.Value > 0)
            {
                await UniTask.Yield();
                _remainingTime.Value -= Time.deltaTime;
            }

            TimerEnd();
        }

        public void TimerEnd()
        {
            IsRunning.Value = false;
            _remainingTime.Value = 0;
            _onTimerEnd.Invoke();
            Debug.Log("Timer has ended.");
        }
    }
}