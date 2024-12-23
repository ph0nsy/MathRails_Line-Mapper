using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public float scale_by = 1.25f;
    public AudioClip hoverSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudioHover()
    {
        source.Play(0);
    }
    
    public void UpscaleHover()
    {
        Vector3 upscaled = new Vector3(this.transform.localScale.x*scale_by, this.transform.localScale.y*scale_by, 1.0f);
        this.transform.localScale = upscaled;
        source.clip = hoverSound;
        source.Play(0);
    }

    public void DownscaleHover()
    {
        Vector3 downscaled = new Vector3(this.transform.localScale.x/scale_by, this.transform.localScale.y/scale_by, 1.0f);
        this.transform.localScale = downscaled;
    }

    public void SwapToggle(){
        Toggle this_toggle = this.transform.parent.gameObject.GetComponent<Toggle>();
        this_toggle.isOn = !this_toggle.isOn;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
        if(this.gameObject.name == "Handle")
            UpscaleHover();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
        if(this.gameObject.name == "Handle")
            DownscaleHover();
    }
}
