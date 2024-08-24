using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityRoomProject.Audio;

public class Guzai : MonoBehaviour
{
    // グザイに付ける
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

        // 色を赤くする
        spriteRenderer.color = hotColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("あたった");

        // 冷風が当たったら色を変え、それをDishに通知する

        if (collision.CompareTag("ColdBless") && !isColded)
        {
            AudioManager.Instance.PlaySE(HikanyanLaboratory.Audio.SEType.冷める音2);
            // 色を白にする
            isColded = true;
            spriteRenderer.color = Color.white;
            coldedGuzai.Invoke(guzaiIndex);
        }
    }
}
