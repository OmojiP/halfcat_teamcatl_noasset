using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArmController : MonoBehaviour
{
    // 腕を操作する
    public HandType handType;

    private Vector3 abPos;
    private Vector3 currentOffset;

    GameObject armParent;
    [SerializeField] HandController handPoint;
    [SerializeField] float handRotateOffset; // 腕の回転にHandの回転の見た目を合わせる(Editor上で調整する)

    private void Start()
    {
        armParent = this.gameObject;
        currentOffset = armParent.transform.position;

        if(handType != handPoint.handType)
        {
            Debug.LogError("handTypeがArmとHandで一致していません");
        }

    }

    public void ArmUpdate()
    {
        abPos = handPoint.transform.position - currentOffset;

        armParent.transform.localScale = new Vector3(abPos.magnitude, 1, 1);
        armParent.transform.rotation = Quaternion.identity;
        float rotateZ = Mathf.Atan2(abPos.y, abPos.x) * 180 / Mathf.PI;
        armParent.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        //armParent.transform.Rotate(0, 0, rotateZ);

        // 腕の向きにHandを合わせる
        handPoint.transform.rotation = Quaternion.Euler(0,0,rotateZ + handRotateOffset);
    }

}
