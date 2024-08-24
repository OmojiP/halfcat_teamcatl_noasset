using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityRoomProject.Audio;

public class BlessGenerator : MonoBehaviour
{
    // ��莞�Ԃ�����BlessGuide���o���ABless�𐶐�
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
            // Eat���Ȃ�҂�
            await UniTask.WaitUntil(() => (eatStewManager != null && eatStewManager.IsExistStew));

            // ��΂������������_���ɑI��
            float shootAngle = Random.Range(30, 150);

            //Debug.Log($"guide{shootAngle}");

            // �K�C�h�����̕����ɍ��킹��
            blessGuide.ShowGuide(shootAngle);
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.�╗�A���[�g);
            // �K�C�h��_��(Animation�ł��)


            await UniTask.Delay(2000);


            // Eat���Ȃ�҂�
            await UniTask.WaitUntil(() => eatStewManager.IsExistStew);

            // �K�C�h������
            blessGuide.DestroyGuide();

            // ColdBless�𐶐����Ĕ�΂�
            var cb = Instantiate(coldBlessPrefab, transform.position, Quaternion.identity, this.transform);
            cb.StartBless(shootAngle, blessEffectsParent);
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.�����O�o���╗);

            await UniTask.Delay(1000);

            

            // 3�b�҂�
            await UniTask.Delay(2500);
        }

        
    }


}
