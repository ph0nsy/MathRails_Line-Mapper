using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0f;
    private bool active = false;
    
    void Awake()
    {
        if(SaveData.instance.firstLoad)
            transition.enabled = true;
        
        if(transition.enabled)
            active = true;
    }

    public IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("Start");
        if(!active)
            transition.enabled = true;
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
}
