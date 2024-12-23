using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FunctionModifiable : MonoBehaviour
{
    public GameObject panelChoosing;
    public GameObject prefabSelected;
    public GameObject prefabModifiable;
    public GameObject prefabSelectableOption;
    public GameObject prefabText;
    public string[] currFunctionA; 
    public string[] currFunctionB; 
    public string currFunctionCombined; 
    private List<int> modifiableSections = new List<int>();
    private (int, int) currOpSelected = new(0,0);
    // Start is called before the first frame update
    void Start()
    {
        currFunctionA = SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_A;
        currFunctionB = SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_B;
        if(currFunctionB.Length > 0 && this.gameObject.name == "FunctionSecondary") 
        {   
            currFunctionCombined = String.Join("", SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_B);

            for (int i = 0; i < currFunctionB.Length; i++)
            {
                GameObject temp;

                if (currFunctionB[i] == "?")
                {
                    modifiableSections.Add(i);
                    temp = Instantiate(prefabModifiable, this.transform);
                } 
                else
                {
                    temp = Instantiate(prefabText, this.transform);
                }
            }
        }
        else if (this.gameObject.name == "Function")
        {
            currFunctionCombined = String.Join("", SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_A);

            for (int i = 0; i < currFunctionA.Length; i++)
            {
                GameObject temp;
                if (i < currFunctionA.Length && currFunctionA[i] == "?")
                {
                    modifiableSections.Add(i);
                    temp = Instantiate(prefabModifiable, this.transform);
                } 
                else
                {
                    temp = Instantiate(prefabText, this.transform);
                }
            }
        }
    }

    public void ShowMathFuncOptions(int index)
    {
        List<GameObject> children = new List<GameObject>();
        foreach(Transform child in panelChoosing.transform)
        {
            children.Add(child.gameObject);
        }

        foreach(GameObject child in children)
        {
            Destroy(child);
        }

        currOpSelected = (gameObject.name == "FunctionSecondary" ? 1 : 0, index);
        panelChoosing.SetActive(true);
        Debug.Log(modifiableSections.Count);
        int startingIdx = 5 * modifiableSections.IndexOf(index);
        int endingIdx = startingIdx + 5;        
        while(startingIdx<endingIdx)
        {
            GameObject temp = Instantiate(prefabSelectableOption, panelChoosing.transform);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currOpSelected.Item1 == 0 ? SaveData.instance.levelsInfo[SaveData.instance.currLvl].posibilities_A[startingIdx] : SaveData.instance.levelsInfo[SaveData.instance.currLvl].posibilities_B[startingIdx];
            startingIdx++;
        }   
    }

    public void CloseMathFuncOptions()
    {
        panelChoosing.SetActive(false);
    }
    
    public void ModifyMathFunction(string text)
    {

        if(currOpSelected.Item1 == 1 && gameObject.name == "FunctionSecondary")
        {
            currFunctionB[currOpSelected.Item2] = text;
            currFunctionCombined = String.Join("", currFunctionB); 
            Destroy(transform.GetChild(currOpSelected.Item2).gameObject);
            GameObject tempInstance = Instantiate(prefabSelected, this.transform);
            tempInstance.transform.SetSiblingIndex(currOpSelected.Item2);
            tempInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            CloseMathFuncOptions();
            GameObject.Find("Rails").gameObject.GetComponent<FunctionDisplay>().UpdateCoords(currFunctionCombined, false);
        }
        
        if (currOpSelected.Item1 == 0 && gameObject.name == "Function")
        {
            currFunctionA[currOpSelected.Item2] = text;
            currFunctionCombined = String.Join("", currFunctionA); 
            Destroy(transform.GetChild(currOpSelected.Item2).gameObject);
            GameObject tempInstance = Instantiate(prefabSelected, this.transform);
            tempInstance.transform.SetSiblingIndex(currOpSelected.Item2);
            tempInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            CloseMathFuncOptions();
            GameObject.Find("Rails").gameObject.GetComponent<FunctionDisplay>().UpdateCoords(currFunctionCombined, true);
        }

    }
}
