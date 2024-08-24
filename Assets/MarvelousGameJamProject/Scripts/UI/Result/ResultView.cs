using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using HikanyanLaboratory.Audio;
using HikanyanLaboratory.SceneManagement;
using MarvelousGameJamProject;
using MarvelousGameJamProject.Result;
using UnityEngine.UI;
using UnityRoomProject.Audio;
using HikanyanLaboratory.Fade;
using MarvelousGameJamProject.System;

public class ResultView : MonoBehaviour
{
    [SerializeField] private GameObject _resultUI;
    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject noTouchCanvas;

    [SerializeField] private RectTransform _resultText;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private GameObject _rankImage;
    [SerializeField] private Text _rankText;
    [SerializeField] private RectTransform _evaluationText;


    [SerializeField] private Sprite _rankS;
    [SerializeField] private Sprite _rankA;
    [SerializeField] private Sprite _rankB;
    [SerializeField] private Sprite _rankC;


    [SerializeField] private string _nextSceneName;
    [SerializeReference, SubclassSelector] private IFadeStrategy _fadeStrategy;

    AudioManager _audioManager;

    // ResultText ((動かす
    // ScoreText ((動かす
    // RankImage((本番 (( 動かす
    // RankText((仮 (( 動かさない
    // EvaluationText ((動かす
    private void Awake()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.StopBGM();
    }

    private void Start()
    {
        titleCanvas.SetActive(false);
        noTouchCanvas.SetActive(true);


        _rankText.text = "";
        _resultUI.transform.localScale = new Vector3(0, 0, 0);
        _resultText.anchoredPosition = new Vector3(0, 1000, 0);
        _evaluationText.localScale = new Vector3(0, 0, 0);
        _rankImage.transform.localScale = new Vector3(0, 0, 0);
        // 各UI要素のアニメーションを設定


        ShowResultUI().Forget();
    }


    public async UniTask ShowResultUI()
    {
        //_audioManager.PlayBGM(BGMType.リザルト画面BGM);

        _audioManager.PlaySE(SEType.選択音SE);
        _resultText.DOAnchorPos(new Vector3(0, -75, 0), 1f).SetEase(Ease.OutBounce);
        await UniTask.Delay(1000);
        _audioManager.PlaySE(SEType.選択音SE);
        _resultUI.transform.localScale = new Vector3(10, 10, 0);
        _resultUI.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        await UniTask.Delay(1000);

        // カウントアップアニメーション 食べた数
        _audioManager.PlaySE(SEType.選択音SEネコ入り);
        _scoreText.transform.localScale = new Vector3(0, 0, 0);
        _scoreText.transform.DOScale(1f, 1.5f).SetEase(Ease.OutBounce);
        ShowScore(StaticSettings.Score);

        await UniTask.Delay(2000);

        // ランク表示アニメーション
        _audioManager.PlaySE(SEType.スネアロール);
        _evaluationText.transform.localScale = new Vector3(0, 0, 0);
        _evaluationText.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

        await UniTask.Delay(3000);
        _rankImage.transform.localScale = new Vector3(20, 20, 0);
        _rankImage.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.OutBounce);
        _rankImage.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
        ShowRank(StaticSettings.Score);
        await UniTask.Delay(2000);

        // _audioManager.PlayBGM(BGMType.リザルト画面BGM);

        titleCanvas.SetActive(true);
        noTouchCanvas.SetActive(false);
    }

    // Scoreをアニメーションで表示する
    // Scoreをアニメーションで表示する
    public void ShowScore(int score)
    {
        var text = _scoreText.GetComponent<Text>();

        // スコアの値を0から指定されたスコアまでカウントアップするアニメーション
        DOVirtual.Float(0, score, 1.5f, value =>
            {
                text.text = $"食べた数 :" +
                            $" {Mathf.FloorToInt(value).ToString()} 杯";
            })
            .SetEase(Ease.OutCubic);
    }

    // Rankをアニメーションで表示する
    public async void ShowRank(int score)
    {
        // スコアに基づいてランクを判定
        RankJudgement.JudgeRank(score);

        // 判定されたランクを取得
        Rank playerRank = StaticSettings.CurrentRank;


        if (playerRank == Rank.S)
        {
            _rankImage.GetComponent<Image>().sprite = _rankS;
            _audioManager.PlaySE(SEType.成功ジングルFat);

            await UniTask.Delay(2000);
            _audioManager.PlayBGM(BGMType.プレイ画面用BGM);
        }
        else if (playerRank == Rank.A)
        {
            _rankImage.GetComponent<Image>().sprite = _rankA;
            _audioManager.PlaySE(SEType.成功ジングルFat);

            await UniTask.Delay(2000);
            _audioManager.PlayBGM(BGMType.プレイ画面用BGM);
        }
        else if (playerRank == Rank.B)
        {
            _rankImage.GetComponent<Image>().sprite = _rankB;
            _audioManager.PlaySE(SEType.失敗ジングル);

            await UniTask.Delay(2000);
            _audioManager.PlayBGM(BGMType.リザルト画面BGM);
        }
        else
        {
            _rankImage.GetComponent<Image>().sprite = _rankC;
            _audioManager.PlaySE(SEType.失敗ジングル);

            await UniTask.Delay(2000);
            _audioManager.PlayBGM(BGMType.リザルト画面BGM);
        }

        // ランクを表示する処理
        Debug.Log($"Player Rank: {playerRank}");
        _rankText.text = playerRank.ToString();
    }

    public void OnGoTitleButton()
    {
        noTouchCanvas.SetActive(true);

        //スコアをリセット
        StaticSettings.Score = 0;

        SceneManager.Instance.LoadSceneWithFade(_nextSceneName, _fadeStrategy).Forget();
    }
}