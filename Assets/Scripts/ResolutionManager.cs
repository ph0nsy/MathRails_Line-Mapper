using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResolutionManager : MonoBehaviour
{
    public List<Vector2> proposedRes;
    private List<Resolution> finalResolutions = new List<Resolution>();
    private List<string> optionsResolutions = new List<string>();
    private TMP_Dropdown resDropdown;

    void OnEnable() 
    {
        if(optionsResolutions.Count == 0){
            foreach(Resolution res in Screen.resolutions) {
                if(proposedRes.Contains(new Vector2(res.width, res.height)) && !optionsResolutions.Contains(res.width + " x " + res.height))
                {
                    finalResolutions.Add(res);
                    optionsResolutions.Add(res.width + " x " + res.height);
                }
            }
            resDropdown = GetComponent<TMP_Dropdown>();
            resDropdown.AddOptions(optionsResolutions);
        }

        int currentRes = 0;
        if(Screen.fullScreen)
            currentRes = optionsResolutions.IndexOf(Screen.currentResolution.width + " x " + Screen.currentResolution.height);
        else 
            currentRes = optionsResolutions.IndexOf(Screen.width + " x " + Screen.height);
        
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    public void UpdateResolution(int index)
    {
        bool wasFullscreen = false;
        if(Screen.fullScreen)
            wasFullscreen = true;
        Screen.SetResolution(finalResolutions[index].width, finalResolutions[index].height, wasFullscreen);
    }

}
