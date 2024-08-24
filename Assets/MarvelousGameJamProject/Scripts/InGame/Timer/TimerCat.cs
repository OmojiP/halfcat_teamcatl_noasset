using MarvelousGameJamProject.InGame;
using UniRx;
using UnityEngine;

public class TimerCat : MonoBehaviour
{
    // TimeStartから制限時間の終わりにかけて動く
    // 動く先はGoalFlag

    // （アニメーション差分が間に合ったら）喫食数に応じて見た目を変える
    [SerializeField] private GameObject goalFlag;
    private TimerPresenter _timerPresenter; // タイマーの残り時間を取得するため 

    private Vector2 startPos;
    private Vector2 dstPos;

    /// <summary>
    /// タイマーの初期化
    /// GameManagerから呼び出す
    /// </summary>
    /// <param name="timerPresenter"></param>
    public void Initialize(TimerPresenter timerPresenter)
    {
        _timerPresenter = timerPresenter;
        startPos = transform.position;
        dstPos = goalFlag.transform.position;

        if (timerPresenter == null)
        {
            Debug.LogError("TimerPresenter is null.");
            return;
        }

        _timerPresenter = timerPresenter;
        if (_timerPresenter.Timer == null)
        {
            Debug.LogError("Timer is null.");
            return;
        }


        // タイマーの残り時間が変わったときに猫の位置を更新
        _timerPresenter.Timer.RemainingTime
            .Subscribe(UpdatePosition)
            .AddTo(this);

        //_timerPresenter.StartTimer();
    }

    /// <summary>
    /// タイマーの残り時間に応じて猫の位置を更新
    /// </summary>
    /// <param name="remainingTime"></param>
    private void UpdatePosition(float remainingTime)
    {
        float currentTime = _timerPresenter.Timer.RemainingTime.Value;

        if (currentTime >= 0 && currentTime <= _timerPresenter.Timer.InitialTime)
        {
            transform.position = startPos + (dstPos - startPos) *
                ((_timerPresenter.Timer.InitialTime - currentTime) / _timerPresenter.Timer.InitialTime);
        }
    }

    /// <summary>
    /// タイマーをストップ
    /// </summary>
    public void TimerStop()
    {
        _timerPresenter.StopTimer();
    }

    /// <summary>
    /// タイマーをリセット
    /// </summary>
    public void TimerReset()
    {
        _timerPresenter.ResetTimer();
        transform.position = startPos;
    }
}