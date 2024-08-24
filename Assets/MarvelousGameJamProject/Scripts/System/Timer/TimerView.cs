using HikanyanLaboratory.System;
using UniRx;
using UnityEngine;

namespace HikanyanLaboratory.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private TMPro.TextMeshProUGUI _timerText;

        private void Start()
        {
            _timer.RemainingTime.Subscribe(time => _timerText.text = $"Time: {time:F2}").AddTo(this);
        }
    }
}