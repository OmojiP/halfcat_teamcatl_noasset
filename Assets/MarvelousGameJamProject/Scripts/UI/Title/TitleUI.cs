using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Audio;
using HikanyanLaboratory.Fade;
using HikanyanLaboratory.SceneManagement;
using MarvelousGameJamProject;
using UnityEngine.UI;
using UnityRoomProject.Audio;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject _noTouchCanvas;
    [SerializeField] private GameObject _explainCanvas;
    [SerializeField] private GameObject[] _explanationUIs;
    [SerializeField] private string _nextSceneName;
    [SerializeReference, SubclassSelector] private IFadeStrategy _fadeStrategy;

    [SerializeField] Toggle invetControlToggle;

    private FadeController _fadeController;
    private AudioManager _audioManager;

    private int currentPage = 0;

    private void Start()
    {
        invetControlToggle.isOn = StaticSettings.IsInvertControl;

        currentPage = 0;
        foreach (var item in _explanationUIs)
        {
            item.SetActive(false);
        }
        _explainCanvas.SetActive(false);
        _noTouchCanvas.SetActive(false);

        _fadeController = FadeController.Instance;
        _audioManager = AudioManager.Instance;
        _audioManager.MasterVolume = 0.2f;
        _audioManager.PlayBGM(BGMType.HalfcatBGM);
    }

    public void OnTitleScreenClicked()
    {
        // 説明UIの表示切り替え
        currentPage = 0;
        _explanationUIs[0].SetActive(true);
        _explainCanvas.SetActive(true);

        _audioManager.PlaySE(SEType.画面切り替わるときの音);
    }

    public void OnClickSceneChangeButton()
    {
        _noTouchCanvas.SetActive(true);
        SceneManager.Instance.LoadSceneWithFade(_nextSceneName, _fadeStrategy).Forget();
    }

    public void OnNextExplainPage(int nextPage)
    {
        if(nextPage >=1 && nextPage < _explanationUIs.Length)
        {
            _audioManager.PlaySE(SEType.画面切り替わるときの音);
            _explanationUIs[currentPage].SetActive(false);
            _explanationUIs[nextPage].SetActive(true);
        }
    }

    public void OnClickButtonSE()
    {
        AudioManager.Instance.PlaySE(SEType.選択音SE);
    }

    public void OnClickToggle(bool isTrue)
    {
        AudioManager.Instance.PlaySE(SEType.選択音SE);
        StaticSettings.IsInvertControl = isTrue;
        Debug.Log(StaticSettings.IsInvertControl);
    }
}