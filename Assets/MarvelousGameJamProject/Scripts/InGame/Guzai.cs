using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityRoomProject.Audio;

public class Guzai : MonoBehaviour
{
    // �O�U�C�ɕt����
    [SerializeField] SpriteRenderer spriteRenderer;

    UnityAction<int> coldedGuzai;
    int guzaiIndex;

    bool isColded;

    public void GuzaiStart(UnityAction<int> _coldedGuzai, int _guzaiIndex , Color hotColor)
    {
        //Debug.Log("GuzaiStart");
        coldedGuzai = _coldedGuzai;
        guzaiIndex = _guzaiIndex;
        isColded = false;

        // �F��Ԃ�����
        spriteRenderer.color = hotColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("��������");

        // �╗������������F��ς��A�����Dish�ɒʒm����

        if (collision.CompareTag("ColdBless") && !isColded)
        {
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.��߂鉹2);
            // �F�𔒂ɂ���
            isColded = true;
            spriteRenderer.color = Color.white;
            coldedGuzai.Invoke(guzaiIndex);
        }
    }
}
