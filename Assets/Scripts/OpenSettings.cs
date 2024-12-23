using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public GameObject menu;
    public AudioClip clickSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OpenPanel(){
        source.clip = clickSound;
        source.Play(0);
        menu.SetActive(true);
    }
    
    public void ClosePanel(){
        source.clip = clickSound;
        source.Play(0);
        menu.SetActive(false);
    }
}
