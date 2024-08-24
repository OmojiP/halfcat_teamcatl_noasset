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
    [SerializeField] float lifeTime; // �u���X��������܂ł̎���
    [SerializeField] int effectCount; // �G�t�F�N�g�������������邩
    [SerializeField] float speed; // �u���X�̑��x
    [SerializeField] float effectRotationOffset; // �G�t�F�N�g�ƃu���X�̉�]�̂���␳
    [SerializeField] float firstEffectSize; // 1�ڂ̃G�t�F�N�g�̑傫��
    [SerializeField] float finalEffectSize; // �Ō�̃G�t�F�N�g�̑傫��

    //�w������ɔ�тȂ������I�ɃG�t�F�N�g���΂�܂�

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
