using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public int this_Lvl;
    private AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
        sceneTransition = GameObject.Find("SceneLoader").GetComponent<SceneTransition>();
        source = GetComponent<AudioSource>();
    }

    public void GotoLevel(){
        source.Play(0);
        SaveData.instance.currLvl = this_Lvl;
        StartCoroutine(sceneTransition.LoadLevel(2));
    }

    public void GotoLevelSelect(){
        source.Play(0);
        StartCoroutine(sceneTransition.LoadLevel(1));
    }
}
