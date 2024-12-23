using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMenuBg : MonoBehaviour
{
    public float offsetMultiplier = 1.0f;
    public float smoothTime = 0.3f;

    private float[] aspecRatios = new float[] {4.0f/3.0f, 16.0f/9.0f, 16.0f/10.0f};
    private float[] offsetModif = new float[] {0.5f, 1.0f, 3.0f};
    private Vector2 startRelativePos;
    private Vector3 speed;
    private float curResolution;

    // Start is called before the first frame update
    void Start()
    {
        startRelativePos = new Vector2 ((float)transform.position.x/(Screen.fullScreen ? (float)Screen.currentResolution.width : (float)Screen.width), (float)transform.position.y/(Screen.fullScreen ? (float)Screen.currentResolution.height : (float)Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        curResolution = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 updatedPos;
        if(Screen.fullScreen)
            updatedPos = new Vector2((float)startRelativePos.x*(float)Screen.currentResolution.width, (float)startRelativePos.y*(float)Screen.currentResolution.height);
        else
            updatedPos = new Vector2((float)startRelativePos.x*(float)Screen.width, (float)startRelativePos.y*(float)Screen.height);
        transform.position = Vector3.SmoothDamp(transform.position, updatedPos + (offset * System.Array.IndexOf(offsetModif, curResolution) * offsetMultiplier), ref speed, smoothTime);
    }
}
