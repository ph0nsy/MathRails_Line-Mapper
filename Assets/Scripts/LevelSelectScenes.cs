using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScenes : MonoBehaviour
{
    private AudioSource source;
    public AudioClip clickSound;
    public SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clickSound;
    }

    public void GotoMainMenu(){
        source.Play(0);
        StartCoroutine(sceneTransition.LoadLevel(0));
    }
}
