using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuOptions : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source;
    public SceneTransition sceneTransition;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clickSound;
    }
    public void CloseGame()
    {
        Debug.Log("Closing Game");
        source.Play(0);
        Application.Quit();
        //EditorApplication.isPlaying = false; // Only while in Editor, comment otherwise
    }

    IEnumerator MenuSettingsAnim(Transform mainMenu, Transform optionsMenu, float speed)
    {   
        Vector3 og_transform = new Vector3(mainMenu.GetChild(0).transform.position.x,mainMenu.GetChild(0).transform.position.y,mainMenu.GetChild(0).transform.position.z);
        mainMenu.GetChild(1).gameObject.SetActive(false);
        while(true)
        {
            if(mainMenu.GetChild(0).transform.position != optionsMenu.transform.GetChild(0).transform.position)
                mainMenu.GetChild(0).transform.position = Vector3.MoveTowards(mainMenu.transform.GetChild(0).transform.position, optionsMenu.transform.GetChild(0).transform.position, speed * Time.deltaTime);
            else 
            {
                mainMenu.GetChild(0).gameObject.SetActive(false);
                optionsMenu.gameObject.SetActive(true);
                mainMenu.GetChild(0).transform.position = og_transform;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator SettingsMenuAnim(Transform mainMenu, Transform optionsMenu, float speed)
    {   
        Vector3 og_transform = new Vector3(optionsMenu.GetChild(0).transform.position.x,optionsMenu.GetChild(0).transform.position.y,optionsMenu.GetChild(0).transform.position.z);
        optionsMenu.GetChild(1).gameObject.SetActive(false);
        optionsMenu.GetChild(2).gameObject.SetActive(false);
        while(true)
        {
            if(optionsMenu.GetChild(0).transform.position != mainMenu.transform.GetChild(0).transform.position)
                optionsMenu.GetChild(0).transform.position = Vector3.MoveTowards(optionsMenu.transform.GetChild(0).transform.position, mainMenu.transform.GetChild(0).transform.position, speed * Time.deltaTime);
            else 
            {
                optionsMenu.GetChild(0).gameObject.SetActive(true);
                optionsMenu.GetChild(1).gameObject.SetActive(true);
                optionsMenu.GetChild(2).gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
                optionsMenu.GetChild(0).transform.position = og_transform;
                mainMenu.GetChild(0).gameObject.SetActive(true);
                mainMenu.GetChild(1).gameObject.SetActive(true);
                yield break;
            }
            yield return null;
        }
    }

    public void LaunchSettings(Transform canvas){
        source.Play(0);
        IEnumerator coroutine = MenuSettingsAnim(canvas.GetChild(2), canvas.GetChild(3), 1000.0f);
        StartCoroutine(coroutine);
    }

    public void BackSettings(Transform canvas){
        source.Play(0);
        IEnumerator coroutine = SettingsMenuAnim(canvas.GetChild(2), canvas.GetChild(3), 1000.0f);
        StartCoroutine(coroutine);
    }


    public void GotoLevelSelect(){
        source.Play(0);
        StartCoroutine(sceneTransition.LoadLevel(1));
    }

    public void GotoCredits(){
        source.Play(0);
        StartCoroutine(sceneTransition.LoadLevel(3));
    }

    public void PlayAudioClick(){
        source.Play(0);
    }
}
