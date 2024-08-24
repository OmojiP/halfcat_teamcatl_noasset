using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ColdBless : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BlessEffect blessEffectPrefab;
    [SerializeField] float lifeTime; // ブレスが消えるまでの時間
    [SerializeField] int effectCount; // エフェクトをいくつ生成するか
    [SerializeField] float speed; // ブレスの速度
    [SerializeField] float effectRotationOffset; // エフェクトとブレスの回転のずれ補正
    [SerializeField] float firstEffectSize; // 1つ目のエフェクトの大きさ
    [SerializeField] float finalEffectSize; // 最後のエフェクトの大きさ

    //指定方向に飛びながら定期的にエフェクトをばらまく

    public void StartBless(float angle, Transform blessEffectsParent)
    {
        MoveBless(angle, blessEffectsParent).Forget();
    }


    async UniTask MoveBless(float angle, Transform blessEffectsParent)
    {
        rb.velocity = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;

        float modifiedAngle = angle + effectRotationOffset;
        var angleQ = Quaternion.Euler(0,0, modifiedAngle);

        for (int i = 0; i < effectCount; i++)
        {
            await UniTask.Delay((int)((lifeTime * 0.9f / effectCount) * 1000));
            var e = Instantiate(blessEffectPrefab, this.transform.position, angleQ, blessEffectsParent);
            e.gameObject.transform.localScale = Vector2.one * (firstEffectSize + (i / (float)effectCount) * (finalEffectSize - firstEffectSize));
        }

        await UniTask.Delay((int)((lifeTime * 0.1f) * 1000));

        Destroy(this.gameObject);
    }
}
