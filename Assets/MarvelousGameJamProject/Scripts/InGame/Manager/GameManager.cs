using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Audio;
using HikanyanLaboratory.Fade;
using HikanyanLaboratory.System;
using HikanyanLaboratory.SceneManagement;
using MarvelousGameJamProject.System;
using UnityEngine;
using UnityEngine.UI;
using UnityRoomProject.Audio;
using UnityRoomProject.System;

namespace MarvelousGameJamProject.InGame
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Text _countDownText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _timerText;
        [SerializeField] private GameEndCats _gameEndCatsPrefab;
        [SerializeField] private float _time;
        [SerializeField] private TimerCat timerCat;

        [SerializeField] EatStewManager eatStewManager;


        [SerializeField] private string _nextSceneName;
        [SerializeReference, SubclassSelector] private IFadeStrategy _fadeStrategy;

        private ScorePresenter _scorePresenter;
        private TimerPresenter _timerPresenter;

        AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
            var score = new Score();
            var timer = new Timer(_time, GameEnd);

            _scorePresenter = new ScorePresenter(score, _scoreText);
            _timerPresenter = new TimerPresenter(timer, _timerText);

            // TimerCatの初期化
            timerCat.Initialize(_timerPresenter);

            _audioManager.PlayBGM(BGMType.HalfcatBGM);
            GameStart();
        }

        public void GameStart()
        {
            _countDownText.text = "";

            _scorePresenter.ResetScore();

            GameStartAsync().Forget();
        }

        public async UniTask GameStartAsync()
        {
            eatStewManager.SetStartState(AddScore);

            //カウントダウン開始
            await UniTask.Delay(1000);
            _audioManager.PlaySE(SEType.ねこカウントダウンBPM60);
            _countDownText.text = "3";
            await UniTask.Delay(1000);

            _countDownText.text = "2";
            await UniTask.Delay(1000);

            _countDownText.text = "1";
            await UniTask.Delay(1000);
            //_audioManager.PlaySE(SEType.開始);
            // Timerの開始
            _countDownText.text = "Start!";
            _timerPresenter.StartTimer();

            // シチューをはじめる
            eatStewManager.GameStart().Forget();
            await UniTask.Delay(1000);
            _countDownText.text = "";
        }

        // Timerが終わると呼ばれる
        public void GameEnd()
        {
            GameEndAsync().Forget();
        }

        async UniTask GameEndAsync()
        {
            //食べ終わるまで待つ
            await UniTask.WaitUntil(() => eatStewManager.IsExistStew);

            eatStewManager.IsExistStew = false;

            //_countDownText.text = "Finish～!";
            _audioManager.PlaySE(SEType.タイムアップ音);

            // 終了演出を流す
            Instantiate(_gameEndCatsPrefab).MoveStart();

            // タイマーの停止 → Timer側で自動で止まる
            //_timerPresenter.StopTimer();

            // シチュー操作を止める
            eatStewManager.GameEnd();
            await UniTask.Delay(2000);

            StaticSettings.Score = _scorePresenter.ScoreValue;

            await SceneManager.Instance.LoadSceneWithFade(_nextSceneName, _fadeStrategy);
        }

        public void GameRestart()
        {
            _scorePresenter.ResetScore();
            GameStart();
        }

        public void AddScore()
        {
            _scorePresenter.AddScore(1);
            //_audioManager.PlaySE(SEType.ごくごく音完飲時);
        }
    }
}