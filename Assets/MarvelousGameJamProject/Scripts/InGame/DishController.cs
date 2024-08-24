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
            Debug.LogError("��𐳂����ݒ肵�Ă�������");
        }
    }

    //�M�̈ʒu���X�V���A���킹�Ď�̈ʒu���X�V����
    public void DishUpdate()
    {
        // hand�ʒu�̏d�S�Ɉړ����A��������]������
        Vector2 left = leftHand.transform.position;
        Vector2 right = rightHand.transform.position;
        RotateDish(left, right);
        TranslateDish(left, right);

        // hand�̋��������l�ɖ߂�
        ModifyHandPosition();
    }


    // �M����]
    void RotateDish(Vector2 left, Vector2 right)
    {
        // �I�_ - �n�_ �ŕ����x�N�g�������߂�
        Vector2 direction = right - left;

        // �A�[�N�^���W�F���g���g�p���Ċp�x�����߁A���W�A������x�ɕϊ�����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // angle��-90~90�Ɏ��܂�悤�ɕ␳����
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Z���𒆐S�ɉ�]������
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Debug.Log($"{angle} �ɉ�]");
    }

    // �M���d�S�Ɉړ�
    void TranslateDish(Vector2 left, Vector2 right)
    {
        transform.position = (left + right) / 2;
    }

    // Dish�̈ʒu�E��]�ɍ��킹��Hand�𒲐�����
    void ModifyHandPosition()
    {
        // Dish�̈ʒu����Dish�̉�]������ dishRadius �i�񂾏ꏊ�� Hand ���ړ�����

        float rot = transform.eulerAngles.z;
        //Debug.Log($"{rot} �ɑ�����");

        Vector2 direction = new Vector2(Mathf.Cos(rot * Mathf.Deg2Rad), Mathf.Sin(rot * Mathf.Deg2Rad));

        leftHand.transform.position = transform.position - (Vector3)(direction * dishRadius);
        rightHand.transform.position = transform.position + (Vector3)(direction * dishRadius);
    }

    // CreateStew
    public void CreateStew()
    {
        // ���̋�ނ�����
        foreach (Transform guzai in guzaiParent)
        {
            Destroy(guzai.gameObject);
        }

        // �V�`���[�����߂�
        isStewColded = false;
        StewSR.color = hotColor;

        //�M���z�𐶐����Ďq�v�f�ɂ���
        //Debug.Log("��ނ̐���");

        // ��ނ������������邩
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

    //��ނ��₦����Ă΂��
    void ColdedGuzai(int i)
    {
        //Debug.Log($"��ނ��₦��{i}");
        coldedGuzaiCount++;

        if(coldedGuzaiCount >= currentGuzais.Length)
        {
            //Debug.Log($"�S�Ă̋�ނ��₦��");
            StewSR.color = Color.white;
            isStewColded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CatHead") && isStewColded)
        {
            //Debug.Log("�₦���X�[�v������");
            isStewColded = false;
            onEatStew.Invoke();
        }
    }
}
