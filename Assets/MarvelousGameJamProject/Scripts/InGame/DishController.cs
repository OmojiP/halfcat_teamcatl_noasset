using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI.Table;

public class DishController : MonoBehaviour
{

    [SerializeField] HandController leftHand;
    [SerializeField] HandController rightHand;

    [SerializeField] SpriteRenderer StewSR;

    [SerializeField] Transform guzaiParent;

    [SerializeField] float dishRadius;

    [SerializeField] Color hotColor;

    [SerializeField] Guzai[] guzaiPrefabs;
    
    public Guzai[] currentGuzais;

    public UnityAction onEatStew;

    private int coldedGuzaiCount = 0;
    private bool isStewColded;

    void Start()
    {
        if(leftHand.handType != HandType.Left || rightHand.handType != HandType.Right)
        {
            Debug.LogError("手を正しく設定してください");
        }
    }

    //皿の位置を更新し、合わせて手の位置も更新する
    public void DishUpdate()
    {
        // hand位置の重心に移動し、向きを回転させる
        Vector2 left = leftHand.transform.position;
        Vector2 right = rightHand.transform.position;
        RotateDish(left, right);
        TranslateDish(left, right);

        // handの距離を一定値に戻す
        ModifyHandPosition();
    }


    // 皿を回転
    void RotateDish(Vector2 left, Vector2 right)
    {
        // 終点 - 始点 で方向ベクトルを求める
        Vector2 direction = right - left;

        // アークタンジェントを使用して角度を求め、ラジアンから度に変換する
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // angleが-90~90に収まるように補正する
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Z軸を中心に回転させる
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Debug.Log($"{angle} に回転");
    }

    // 皿を重心に移動
    void TranslateDish(Vector2 left, Vector2 right)
    {
        transform.position = (left + right) / 2;
    }

    // Dishの位置・回転に合わせてHandを調整する
    void ModifyHandPosition()
    {
        // Dishの位置からDishの回転方向に dishRadius 進んだ場所に Hand を移動する

        float rot = transform.eulerAngles.z;
        //Debug.Log($"{rot} に揃える");

        Vector2 direction = new Vector2(Mathf.Cos(rot * Mathf.Deg2Rad), Mathf.Sin(rot * Mathf.Deg2Rad));

        leftHand.transform.position = transform.position - (Vector3)(direction * dishRadius);
        rightHand.transform.position = transform.position + (Vector3)(direction * dishRadius);
    }

    // CreateStew
    public void CreateStew()
    {
        // 元の具材を消す
        foreach (Transform guzai in guzaiParent)
        {
            Destroy(guzai.gameObject);
        }

        // シチューを温める
        isStewColded = false;
        StewSR.color = hotColor;

        //熱い奴を生成して子要素にする
        //Debug.Log("具材の生成");

        // 具材をいくつ生成するか
        coldedGuzaiCount = 0;
        int instRandom = Random.Range(2, 5);
        currentGuzais = new Guzai[instRandom];

        for (int i = 0; i < instRandom; i++)
        {
            int guzaiRandom = Random.Range(0, guzaiPrefabs.Length);
            float radiusRamdom = Random.Range(dishRadius * 0.1f, dishRadius * 0.6f);
            float angleRandom = Mathf.PI * 2 / instRandom * i;
            float rotateRandom = Random.Range(0, 180);
            Vector2 instPos = this.transform.position + new Vector3(radiusRamdom * Mathf.Cos(angleRandom), radiusRamdom * Mathf.Sin(angleRandom), 0);
            currentGuzais[i] = Instantiate(guzaiPrefabs[guzaiRandom], instPos, Quaternion.Euler(0, 0, rotateRandom), guzaiParent);
            currentGuzais[i].GuzaiStart(ColdedGuzai, i, hotColor);
        }
    }

    //具材が冷えたら呼ばれる
    void ColdedGuzai(int i)
    {
        //Debug.Log($"具材が冷えた{i}");
        coldedGuzaiCount++;

        if(coldedGuzaiCount >= currentGuzais.Length)
        {
            //Debug.Log($"全ての具材が冷えた");
            StewSR.color = Color.white;
            isStewColded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CatHead") && isStewColded)
        {
            //Debug.Log("冷えたスープを飲む");
            isStewColded = false;
            onEatStew.Invoke();
        }
    }
}
