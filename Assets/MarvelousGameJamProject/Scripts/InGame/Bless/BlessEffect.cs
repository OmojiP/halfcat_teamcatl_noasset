using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessEffect : MonoBehaviour
{
    // Bless‚Ì‚¢‚½êŠ‚É¶¬‚³‚êAˆê’èŠÔ‚ÅÁ‚¦‚é

    private void Start()
    {
        PlayEffect().Forget();
    }


    private async UniTask PlayEffect()
    {
        await UniTask.Delay(1000);

        Destroy(gameObject);
    }
}
