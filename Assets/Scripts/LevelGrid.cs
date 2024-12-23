using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGrid : MonoBehaviour
{
    public GameObject levelGradedPrefab;
    public GameObject levelBasicPrefab;
    public GameObject levelLockedPrefab;
    private List<GameObject> levelsOnGrid = new List<GameObject>(25);
    private string[] scores = {"F", "C", "B", "A"};
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SaveData.instance.levelsStatus["unlocked"].Count; i++){
            if(SaveData.instance.levelsStatus["unlocked"][i] > 0 || (i > 0 && SaveData.instance.levelsStatus["graded"][i-1] > 0)){
                if(SaveData.instance.levelsStatus["graded"][i] > 0){
                    GameObject tempLevel = Instantiate(levelGradedPrefab, transform);
                    tempLevel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = SaveData.instance.levelsInfo[i].levelName;
                    tempLevel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = scores[SaveData.instance.levelsStatus["gradeGiven"][i]];
                    tempLevel.GetComponent<LoadLevel>().this_Lvl = i;
                    levelsOnGrid.Add(tempLevel);
                }
                else
                {
                    SaveData.instance.levelsStatus["unlocked"][i] = 1;
                    GameObject tempLevel = Instantiate(levelBasicPrefab, transform);
                    tempLevel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = SaveData.instance.levelsInfo[i].levelName;
                    tempLevel.GetComponent<LoadLevel>().this_Lvl = i;
                    levelsOnGrid.Add(tempLevel);
                }
            }
            else
            {
                GameObject tempLevel = Instantiate(levelLockedPrefab, transform);
                tempLevel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "???";
                levelsOnGrid.Add(tempLevel);
            }        
        }
    }
}
