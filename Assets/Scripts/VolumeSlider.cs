using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{

    public AudioMixer audioMixer;
    public GameObject muteObj;
    public GameObject soundObj;
    public float currentVol = -10.0f;
    Slider slider;
    public bool music = false;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if(music){ 
            PlayerPrefs.SetFloat("MusicVolume", currentVol);
            slider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else {
            PlayerPrefs.SetFloat("EffectVolume", currentVol);
            slider.value = PlayerPrefs.GetFloat("EffectVolume");
        }
    }

    public void UpdateVolume()
    {
        if (music){
            PlayerPrefs.SetFloat("MusicVolume", slider.value);
            audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));
            if(muteObj.activeSelf && slider.value > -50.0f)
            {
                audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));
                soundObj.SetActive(true);
                muteObj.SetActive(false);
            }
            if(!muteObj.activeSelf && slider.value == -50.0f)
            {
                audioMixer.SetFloat("MusicVol", -80.0f);
                muteObj.SetActive(true);
                soundObj.SetActive(false);
            }
        }
        else{
            PlayerPrefs.SetFloat("EffectVolume", slider.value);
            audioMixer.SetFloat("SEVol", PlayerPrefs.GetFloat("EffectVolume"));
            if(muteObj.activeSelf && slider.value > -50.0f)
            {
                audioMixer.SetFloat("SEVol", PlayerPrefs.GetFloat("EffectVolume"));
                soundObj.SetActive(true);
                muteObj.SetActive(false);
            }
            if(!muteObj.activeSelf && slider.value == -50.0f)
            {
                audioMixer.SetFloat("SEVol", -80.0f);
                muteObj.SetActive(true);
                soundObj.SetActive(false);
            }
        }
        
    }

    public void ToggleMute()
    {
        if (music){
            if(soundObj.activeSelf)
            {
                soundObj.SetActive(false);
                muteObj.SetActive(true);
                PlayerPrefs.SetFloat("MusicVolume", slider.value);
                slider.value = -80.0f;
                audioMixer.SetFloat("MusicVol", slider.value);
            } 
            else 
            {
                muteObj.SetActive(false);
                soundObj.SetActive(true);
                if(PlayerPrefs.GetFloat("MusicVolume") == -50.0f)
                    slider.value = -49.0f;
                else
                    slider.value = PlayerPrefs.GetFloat("MusicVolume");
                audioMixer.SetFloat("MusicVol", slider.value);
            }
        }
        else{
            
            if(soundObj.activeSelf)
            {
                soundObj.SetActive(false);
                muteObj.SetActive(true);
                PlayerPrefs.SetFloat("EffectVolume", slider.value);
                slider.value = -80.0f;
                audioMixer.SetFloat("SEVol", slider.value);
            } 
            else 
            {
                muteObj.SetActive(false);
                soundObj.SetActive(true);
                if(PlayerPrefs.GetFloat("EffectVolume") == -50.0f)
                    slider.value = -49.0f;
                else
                    slider.value = PlayerPrefs.GetFloat("EffectVolume");
                audioMixer.SetFloat("SEVol", slider.value);
            }
        }
    }
}
