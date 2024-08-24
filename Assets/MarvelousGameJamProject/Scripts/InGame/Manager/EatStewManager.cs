using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;
using UnityRoomProject.Audio;

public class EatStewManager : MonoBehaviour
{

    /*

    EatStewManager

    カウントダウン

    Stewの生成

    冷えたStewがネコに当たったら皿を画面下に隠し、右上のeatアニメーションを再生

    次のStewを生成し、画面真ん中に表示

    */

    [SerializeField] HandController[] hands;
    [SerializeField] DishController dish;

    [SerializeField] Animator rightTopCatAnimator;
    [SerializeField] ControllerUpdater controllerUpdater;

    public bool IsExistStew { get { return controllerUpdater.isExistStew; }  set { controllerUpdater.isExistStew = value; } }
    UnityAction addScore;

    private void Start()
    {
        controllerUpdater.isExistStew = false;
        dish.onEatStew = EatStew;
    }

    public async UniTask GameStart()
    {
        Debug.Log("GameStart");
        await CreateStew();
    }

    // 初期状態にする
    public void SetStartState(UnityAction _addScore)
    {
        // addScoreを食べた時に呼ぶように設定する
        addScore = _addScore;

        // dishを画面下に隠す
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].SetPosOutOfScreen();
        }
        
        // ネコアニメーションをStartで待機

    }

    async UniTask CreateStew()
    {
        // Stewを作成し、下から表示

        //Stew作成
        dish.CreateStew();

        await UniTask.Delay(500);

        // 下から表示させる動きはHandに関数を用意する
        UniTask[] tasks = new UniTask[hands.Length];
        for (int i = 0; i < hands.Length; i++)
        {
             tasks[i] = hands[i].MoveHandCenterOfScreen();
        }

        rightTopCatAnimator?.SetTrigger("BringStew");

        await UniTask.WhenAll(tasks);

        controllerUpdater.isExistStew = true;
    }

    void EatStew()
    {
        EatStewAsync().Forget();
    }

    async UniTask EatStewAsync()
    {
        // 操作を無効化する
        controllerUpdater.isExistStew = false;

        // StewをHandの関数を使用して下画面に隠す
        UniTask[] tasks = new UniTask[hands.Length];
        for (int i = 0; i < hands.Length; i++)
        {
            tasks[i] = hands[i].MoveHandOutOfScreen();
        }

        await UniTask.WhenAll(tasks);
        // 右上のeatアニメーションを呼び出す

        AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.ごくごく音完飲時);
        rightTopCatAnimator?.SetTrigger("EatStew");

        await UniTask.Delay(500);

        addScore.Invoke();

        // CreateSteweを呼び出す
        CreateStew().Forget();
    }

    public void GameEnd()
    {
        controllerUpdater.isExistStew = false;

    }
}
