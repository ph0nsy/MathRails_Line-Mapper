using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FullscreenToggle : MonoBehaviour
{
    void OnEnable()
    {
        transform.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
    }
}
