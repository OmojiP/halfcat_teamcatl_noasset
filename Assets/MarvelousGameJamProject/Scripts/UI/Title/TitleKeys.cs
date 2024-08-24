using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleKeys : MonoBehaviour
{
    [SerializeField] Image w;
    [SerializeField] Image a;
    [SerializeField] Image s;
    [SerializeField] Image d;
    [SerializeField] Image up;
    [SerializeField] Image left;
    [SerializeField] Image down;
    [SerializeField] Image right;

    [SerializeField] Color onColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        w.color = Color.white;
        a.color = Color.white;
        s.color = Color.white;
        d.color = Color.white;
        up.color = Color.white;
        left.color = Color.white;
        down.color = Color.white;
        right.color = Color.white;


        if (Input.GetKey(KeyCode.W))
        {
            w.color = onColor;
        }
        if (Input.GetKey(KeyCode.A))
        {
            a.color = onColor;
        }
        if (Input.GetKey(KeyCode.S))
        {
            s.color = onColor;
        }
        if (Input.GetKey(KeyCode.D))
        {
            d.color = onColor;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up.color = onColor;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left.color = onColor;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            down.color = onColor;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right.color = onColor;
        }
    }
}
