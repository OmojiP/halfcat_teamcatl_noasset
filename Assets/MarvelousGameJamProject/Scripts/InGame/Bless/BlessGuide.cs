using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
public class BlessGuide : MonoBehaviour
{
    [SerializeField] SpriteRenderer guideSR;

    private void Start()
    {
        guideSR.enabled = false;
    }

    public void ShowGuide(float angle)
    {
        // angleにガイドを伸ばす
        this.transform.localScale = new Vector3(20, 1, 1);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // spriterenderer の enableで表示・非表示を切り替える
        guideSR.enabled = true;

    }

    public void DestroyGuide()
    {
        // spriterenderer の enableで表示・非表示を切り替える
        guideSR.enabled = false;
    }
}
