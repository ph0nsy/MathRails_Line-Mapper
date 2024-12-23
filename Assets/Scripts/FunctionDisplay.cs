using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class FunctionDisplay : MonoBehaviour
{

    public GameObject prefabRails;
    public GameObject prefabPointsPrimary;
    public GameObject prefabPointsSecondary;
    public GameObject prefabStationPrimary;
    public GameObject prefabStationSecondary;
    Vector2 size;
    public List<(double, double)> currCoords = new List<(double, double)>();
    public List<(double, double)> currCoordsSecond = new List<(double, double)>();
    public string solFunc;
    public string solFuncSecond;
    public List<(double, double)> solCoords = new List<(double, double)>();
    public List<(double, double)> solCoordsSecond = new List<(double, double)>();
    public List<(double, double)> givenCoords = new List<(double, double)>();
    public List<(double, double)> givenCoordsSecond = new List<(double, double)>();
    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2 (transform.parent.GetComponent<RectTransform>().rect.width, transform.parent.GetComponent<RectTransform>().rect.height);
        
        size.x = transform.parent.GetComponent<RectTransform>().rect.width;
        size.y = transform.parent.GetComponent<RectTransform>().rect.height;
        GetComponent<RectTransform>().sizeDelta = size;

        string solFunc = String.Join("", SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_A);
        if(solFunc.Length > 0){
            int countModif = solFunc.Split('?').Length - 1;
            for(int i = 0; i<countModif; i++){
                var regex = new Regex(Regex.Escape("?"));
                solFunc = regex.Replace(solFunc, SaveData.instance.levelsInfo[SaveData.instance.currLvl].posibilities_A[SaveData.instance.levelsInfo[SaveData.instance.currLvl].solutions_A[i]], 1);
            }
        }

        solFuncSecond = String.Join("", SaveData.instance.levelsInfo[SaveData.instance.currLvl].mathFunction_B);
        if(solFuncSecond.Length > 0){
            int countModif = solFuncSecond.Split('?').Length - 1;
            for(int i = 0; i<countModif; i++){
                var regex = new Regex(Regex.Escape("?"));
                solFuncSecond = regex.Replace(solFuncSecond, SaveData.instance.levelsInfo[SaveData.instance.currLvl].posibilities_B[SaveData.instance.levelsInfo[SaveData.instance.currLvl].solutions_B[i]], 1);
            }
        }

        for(int i = 0; i < 21; i++)
        {
            if(solFunc[0] == 'y')
                solCoords.Add(new (i-10,CalculateStringAtX(i-10, solFunc)));
            else
                solCoords.Add(new (CalculateStringAtY(i-10, solFunc),i-10));

            if(solFuncSecond.Length > 0){
                if(solFuncSecond[0] == 'y')
                    solCoordsSecond.Add(new (i-10,CalculateStringAtX(i-10, solFuncSecond)));
                else
                    solCoordsSecond.Add(new (CalculateStringAtY(i-10, solFuncSecond),i-10));
            }
        }

        for(int i = 0; i < 3; i++){
            int idxRnd = UnityEngine.Random.Range(0, 21);
            while(givenCoords.Contains(solCoords[idxRnd])){
                idxRnd = UnityEngine.Random.Range(0, 21);
            }
            givenCoords.Add(solCoords[idxRnd]);
            if(solFuncSecond.Length > 0){
                idxRnd = UnityEngine.Random.Range(0, 21);
                while(givenCoordsSecond.Contains(solCoordsSecond[idxRnd])){
                    idxRnd = UnityEngine.Random.Range(0, 21);
                }
                givenCoordsSecond.Add(solCoordsSecond[idxRnd]);
            }
        }

        for(int i = 0; i < givenCoords.Count; i++){
            GameObject currentDot1, currentDot2;
            currentDot1 = Instantiate(prefabStationPrimary, GameObject.Find("Stations").transform);
            currentDot1.GetComponent<RectTransform>().localPosition = new Vector3((float)givenCoords[i].Item1*(size.x/22.0f), (float)givenCoords[i].Item2*(size.y/22.0f), 0.0f);
             if(solFuncSecond.Length > 0){
                currentDot2 = Instantiate(prefabStationSecondary, GameObject.Find("Stations").transform);
                currentDot2.GetComponent<RectTransform>().localPosition = new Vector3((float)givenCoordsSecond[i].Item1*(size.x/22.0f), (float)givenCoordsSecond[i].Item2*(size.y/22.0f), 0.0f);
             }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        // Update size
        size.x = transform.parent.GetComponent<RectTransform>().rect.width;
        size.y = transform.parent.GetComponent<RectTransform>().rect.height;
        List<GameObject> children = new List<GameObject>();
        foreach(Transform child in this.transform)
        {
            children.Add(child.gameObject);
        }
        
        foreach(Transform child in GameObject.Find("Stations").transform)
        {
            children.Add(child.gameObject);
        }

        foreach(GameObject child in children)
        {
            Destroy(child);
        }
        
        GetComponent<RectTransform>().sizeDelta = size;

        for(int i = 0; i < givenCoords.Count; i++){
            GameObject currentDot1, currentDot2;
            currentDot1 = Instantiate(prefabStationPrimary, GameObject.Find("Stations").transform);
            currentDot1.GetComponent<RectTransform>().localPosition = new Vector3((float)givenCoords[i].Item1*(size.x/22.0f), (float)givenCoords[i].Item2*(size.y/22.0f), 0.0f);
             if(solFuncSecond.Length > 0){
                currentDot2 = Instantiate(prefabStationSecondary, GameObject.Find("Stations").transform);
                currentDot2.GetComponent<RectTransform>().localPosition = new Vector3((float)givenCoordsSecond[i].Item1*(size.x/22.0f), (float)givenCoordsSecond[i].Item2*(size.y/22.0f), 0.0f);
             }
        } 

        PositionAll(currCoords, true);
        PositionAll(currCoordsSecond, false);
    }

    public void UpdateCoords(string currFunc, bool mainFunc){
        if(mainFunc)
            currCoords = new List<(double, double)>();
        else
            currCoordsSecond = new List<(double, double)>();
        
        if(!currFunc.Contains("?"))
        {
            for(int i = 0; i < 21; i++)
            {
                if(currFunc[0] == 'y' && mainFunc)
                    currCoords.Add(new (i-10,CalculateStringAtX(i-10, currFunc)));
                else if(currFunc[0] == 'y' && !mainFunc)
                    currCoordsSecond.Add(new (i-10,CalculateStringAtX(i-10, currFunc)));
                else if(mainFunc)
                    currCoords.Add(new (CalculateStringAtY(i-10, currFunc),i-10));
                else
                    currCoordsSecond.Add(new (CalculateStringAtY(i-10, currFunc),i-10));
            }
        }
    }

    double CalculateStringAtX(double x, string equation)
    { 
        double value = 0;
        string tempEq = equation.Remove(0,2);
        char[] charEq = tempEq.ToCharArray();
        for(int i = 0; i < charEq.Length; i++){
            if (i == 0)
            {
                if(charEq[0] == '-'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                                value -= (double)Mathf.Pow((float)x,2.0f);
                                i++;
                            }
                        else
                            value -= x;
                    }
                    else 
                        value = -1 * Convert.ToDouble(charEq[1].ToString());
                    i++;
                }
                else if (charEq[i] == 'x' && charEq[i+1] == '^'){
                    value = (double)Mathf.Pow((float)x,2.0f);
                    i++;
                }
                else
                    value = charEq[0] == 'x' ? x : Convert.ToDouble(charEq[0].ToString());
            }
            else if (charEq[i] == '+' || charEq[i] == '-' || charEq[i] == '*' || charEq[i] == '/') 
            {

                if(charEq[i] == '+'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value += (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value += x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value -= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value -= x;
                        i++;
                    }
                    else 
                        value += Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }

                if(charEq[i] == '-'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value -= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value -= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value += (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value += x;
                        i++;
                    }
                    else 
                        value -= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '*'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value *= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value *= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value *= -(double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value *= -x;
                        i++;
                    }
                    else 
                        value *= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '/'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value /= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value /= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value /= -(double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value /= -x;
                        i++;
                    }
                    else 
                        value /= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }       
            }
            
        }
        return value; 
    }
    
    double CalculateStringAtY(double y, string equation)
    { 
        double value = 0;
        string tempEq = equation.Remove(0,2);
        char[] charEq = tempEq.ToCharArray();
        for(int i = 0; i < charEq.Length; i++){
            if (i == 0)
            {
                if(charEq[0] == '-'){
                    if(charEq[i+1] == 'y'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                                value -= (double)Mathf.Pow((float)y,2.0f);
                                i++;
                            }
                        else
                            value -= y;
                    }
                    else 
                        value = -1 * Convert.ToDouble(charEq[1].ToString());
                    i++;
                }
                else if (charEq[i] == 'y' && charEq[i+1] == '^'){
                    value = (double)Mathf.Pow((float)y,2.0f);
                    i++;
                }
                else
                    value = charEq[0] == 'y' ? y : Convert.ToDouble(charEq[0].ToString());
            }
            else if (charEq[i] == '+' || charEq[i] == '-' || charEq[i] == '*' || charEq[i] == '/') 
            {

                if(charEq[i] == '+'){
                    if(charEq[i+1] == 'y'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value += (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value += y;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'y'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value -= (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value -= y;
                        i++;
                    }
                    else 
                        value += Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }

                if(charEq[i] == '-'){
                    if(charEq[i+1] == 'y'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value -= (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value -= y;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'y'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value += (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value += y;
                        i++;
                    }
                    else 
                        value -= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '*'){
                    if(charEq[i+1] == 'y'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value *= (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value *= y;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'y'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value *= -(double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value *= -y;
                        i++;
                    }
                    else 
                        value *= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '/'){
                    if(charEq[i+1] == 'y'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value /= (double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value /= y;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'y'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value /= -(double)Mathf.Pow((float)y,2.0f);
                            i++;
                        }
                        else
                            value /= -y;
                        i++;
                    }
                    else 
                        value /= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }       
            }
            
        }
        return value; 
    }

    void PositionAll(List<(double, double)> coords, bool mainFunc){
        // PositionRails
        for(int i = 0; i < coords.Count - 1; i++){
            GameObject currentRail = Instantiate(prefabRails, this.transform);
            if(coords[i].Item1 == coords[i+1].Item1)
                currentRail.GetComponent<RectTransform>().localPosition = new Vector3((float)coords[i].Item1*(size.x/22.0f), (float)((((float)coords[i].Item2*(size.y/22.0f)+(float)coords[i+1].Item2*(size.y/22.0f))/2.0f)), 0.0f);
            else if(coords[i].Item2 == coords[i+1].Item2)
                currentRail.GetComponent<RectTransform>().localPosition = new Vector3((float)((((float)coords[i].Item1+(float)coords[i+1].Item1)/2.0f)*(size.x/22.0f)), (float)coords[i].Item2*(size.y/22.0f), 0.0f);
            else
                currentRail.GetComponent<RectTransform>().localPosition = new Vector3((float)((((float)coords[i].Item1+(float)coords[i+1].Item1)/2.0f)*(size.x/22.0f)), (float)((((float)coords[i].Item2*(size.y/22.0f)+(float)coords[i+1].Item2*(size.y/22.0f))/2.0f)), 0.0f);

            Vector3 rotation = currentRail.GetComponent<RectTransform>().eulerAngles;
            float rotZ = -Mathf.Atan((float)(coords[i].Item1-coords[i+1].Item1)/(float)(coords[i].Item2-coords[i+1].Item2));
            rotation.z = Mathf.Abs(rotZ) <= 5 ? 90 : rotZ;
            currentRail.GetComponent<RectTransform>().eulerAngles = rotation;
            Vector2 currSize = new Vector2(transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x, Mathf.Sqrt(Mathf.Pow((float)(coords[i].Item1-coords[i+1].Item1),2.0f) + Mathf.Pow((float)(coords[i].Item2-coords[i+1].Item2),2.0f)));
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = currSize;
        } 
        // Position Dots
        for(int i = 0; i < coords.Count; i++){
            GameObject currentDot;
            if(mainFunc)
                currentDot = Instantiate(prefabPointsPrimary, this.transform);
            else
                currentDot = Instantiate(prefabPointsSecondary, this.transform);
            currentDot.GetComponent<RectTransform>().localPosition = new Vector3((float)coords[i].Item1*(size.x/22.0f), (float)coords[i].Item2*(size.y/22.0f), 0.0f);
        } 
    }

}
