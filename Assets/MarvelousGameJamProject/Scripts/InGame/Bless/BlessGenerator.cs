using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityRoomProject.Audio;

public class BlessGenerator : MonoBehaviour
{
    // 一定時間おきにBlessGuideを出し、Blessを生成
    [SerializeField] BlessGuide blessGuide;
    [SerializeField] ColdBless coldBlessPrefab;
    [SerializeField] Transform blessEffectsParent;
    [SerializeField] EatStewManager eatStewManager;

    private void Start()
    {
        GenerateBless();
    }

    private void GenerateBless()
    {
        GenerateBlessAsync().Forget();
    }

    private async UniTask GenerateBlessAsync()
    {
        while (true)
        {
            // Eat中なら待つ
            await UniTask.WaitUntil(() => (eatStewManager != null && eatStewManager.IsExistStew));

            // 飛ばす方向をランダムに選択
            float shootAngle = Random.Range(30, 150);

            //Debug.Log($"guide{shootAngle}");

            // ガイドをその方向に合わせる
            blessGuide.ShowGuide(shootAngle);
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.冷風アラート);
            // ガイドを点滅(Animationでやる)


            await UniTask.Delay(2000);


            // Eat中なら待つ
            await UniTask.WaitUntil(() => eatStewManager.IsExistStew);

            // ガイドを消す
            blessGuide.DestroyGuide();

            // ColdBlessを生成して飛ばす
            var cb = Instantiate(coldBlessPrefab, transform.position, Quaternion.identity, this.transform);
            cb.StartBless(shootAngle, blessEffectsParent);
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.リング出現冷風);

            await UniTask.Delay(1000);

            

            // 3秒待つ
            await UniTask.Delay(2500);
        }

        
    }


}
