using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitOption : MonoBehaviour
{
    FunctionModifiable parentFunction;
    FunctionModifiable parentFunctionSecond;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        parentFunction = GameObject.Find("Function").gameObject.GetComponent<FunctionModifiable>();
        parentFunctionSecond = GameObject.Find("FunctionSecondary").gameObject.GetComponent<FunctionModifiable>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    public void SelectThis(){
        parentFunction.ModifyMathFunction(text);
    }
    
    public void OpenOptions()
    {
        int i = transform.GetSiblingIndex();
        if(transform.parent.name == "FunctionSecondary")
            parentFunctionSecond.ShowMathFuncOptions(i);
        else            
            parentFunction.ShowMathFuncOptions(i);
    }

    public void CloseOptions()
    {
        if(transform.parent.name == "FunctionSecondary")
            parentFunctionSecond.CloseMathFuncOptions();
        else            
            parentFunction.CloseMathFuncOptions();
    }
}
