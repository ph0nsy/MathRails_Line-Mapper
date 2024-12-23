using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSizeAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 size = new Vector2 (GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
        transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((size.x/2.0f)-2.0f, (size.y/2.0f)-2.0f);
        transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((size.x/2.0f)-2.0f, (size.y/2.0f)-2.0f);
        transform.GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2((size.x/2.0f)-2.0f, (size.y/2.0f)-2.0f);
        transform.GetChild(0).GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2((size.x/2.0f)-2.0f, (size.y/2.0f)-2.0f);
        if(transform.childCount > 1)
            transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, 4);
        if(transform.childCount > 2)
            transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(4, size.y);
    }
}
