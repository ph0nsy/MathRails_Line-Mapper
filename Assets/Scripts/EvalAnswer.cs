using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalAnswer : MonoBehaviour
{
    public FunctionDisplay functionData;
    public SceneTransition transition;
    private List<(double,double)> pointsPrimary;
    private List<(double,double)> pointsSecondary;
    private List<(double,double)> expectedPrimary;
    private List<(double,double)> expectedSecondary;
    private List<(double,double)> providedPrimary;
    private List<(double,double)> providedSecondary;

    // Start is called before the first frame update
    void Start()
    {
        pointsPrimary = functionData.currCoords;
        pointsSecondary = functionData.currCoordsSecond;
        expectedPrimary = functionData.solCoords;
        expectedSecondary = functionData.solCoordsSecond;
        providedPrimary = functionData.givenCoords;
        providedSecondary = functionData.givenCoordsSecond;
    }

    float Distance2Points((double,double) first, (double,double) second){ // Euclidean Distance
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow((float)second.Item1-(float)first.Item1,2)+Mathf.Pow((float)second.Item2-(float)first.Item2,2)));
    }
    
    bool ListAreSame(List<(double,double)> l1, List<(double,double)> l2){
        if (l1.Count != l2.Count)
            return false;
        
        l1.Sort();
        l2.Sort();
        
        for(int i = 0; i < l1.Count; i++){
            if(!l1[i].Equals(l2[i]))
                return false;             
        }

        return true;
    }

    float MSE(List<(double,double)> expected, List<(double,double)> provided)
    {
        float value = 0;
        for(int i = 0; i < expected.Count; i++){
            value += Distance2Points(expected[i],provided[i]);
        }
        return value/expected.Count;
    }
    
    float ArcLength(List<(double,double)> set){
        float value = 0;
        for(int i = 0; i < set.Count - 1; i++){
            value += Distance2Points(set[i],set[i+1]);
        }
        return value;
    }

    float DTW(List<(double,double)> expected, List<(double,double)> provided){
        int n = expected.Count;
        int m = provided.Count;

        float [,] dtwMatrix = new float[n + 1, m + 1];
        for (int i = 0; i < n + 1; i++)
            for (int j = 0; j < m + 1; j++)
                dtwMatrix[i, j] = float.PositiveInfinity;

        dtwMatrix[0, 0] = 0;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                float cost = Distance2Points(expected[i - 1], provided[j - 1]);
                dtwMatrix[i, j] = cost + Mathf.Min(
                    Mathf.Min(dtwMatrix[i - 1, j],     // Insertion
                            dtwMatrix[i, j - 1]),     // Deletion
                            dtwMatrix[i - 1, j - 1]); // Match
            }
        }
        return dtwMatrix[n, m];
    }

    // Update is called once per frame
    public void EvaluateOption()
    {
        pointsPrimary = functionData.currCoords;
        pointsSecondary = functionData.currCoordsSecond;
        expectedPrimary = functionData.solCoords;
        expectedSecondary = functionData.solCoordsSecond;
        providedPrimary = functionData.givenCoords;
        providedSecondary = functionData.givenCoordsSecond;

        if(pointsPrimary.Count > 0)
            return;
        else{
            int grade = 0;
            bool isA = true;
            if(ArcLength(pointsPrimary) <= ArcLength(expectedPrimary))
            {
                if(ArcLength(pointsSecondary) <= ArcLength(expectedSecondary))
                {
                    foreach((int,int) point in providedSecondary) 
                    {
                        if(!pointsSecondary.Contains(point))
                            isA = false;
                            break;
                    }
                }
                foreach((int,int) point in providedPrimary) 
                {
                    if(!pointsPrimary.Contains(point))
                        isA = false;
                        break;
                }
            }
            else if(ListAreSame(pointsPrimary, expectedPrimary))
            {
                if(providedSecondary.Count > 0)
                {
                    if(!ListAreSame(pointsSecondary, expectedSecondary))
                        isA = false;
                }
            }
            else
            {
                isA = false;

                float ecm = MSE(expectedPrimary, pointsPrimary);
                if(providedSecondary.Count > 0)
                    ecm = (ecm + MSE(pointsSecondary, expectedSecondary))/2;

                float arcDif = ArcLength(pointsPrimary) - ArcLength(expectedPrimary);
                if(providedSecondary.Count > 0)
                    arcDif = (arcDif + ArcLength(pointsSecondary) - ArcLength(expectedSecondary))/2;
                
                float dtw = DTW(expectedPrimary, pointsPrimary);
                if(providedSecondary.Count > 0)
                    dtw = (dtw + DTW(pointsSecondary, expectedSecondary))/2;
                
                grade += ecm > 1 ? -1 : ecm > 0.5 ? 0 : 1;
                grade += arcDif > 3 ? -1 : arcDif > 0 ? 0 : 1;
                grade += (dtw > 0.75 && dtw < -0.75) ? -1 : (dtw > 0.5 && dtw < -0.5) ? 0 : 1;
            }

            if(isA){ grade = 3; }
            else if (grade > 1) { grade = 2; }
            else if (grade > -1) { grade = 1; }
            else { grade = 0; }

            SaveData.instance.levelsStatus["graded"][SaveData.instance.currLvl] = 1;
            SaveData.instance.levelsStatus["gradeGiven"][SaveData.instance.currLvl] = grade;
            if(grade > 0 && SaveData.instance.levelsStatus["graded"].Count > SaveData.instance.currLvl+1) 
                SaveData.instance.levelsStatus["unlocked"][SaveData.instance.currLvl+1] = 1;
            SaveData.instance.Save(SaveData.instance.levelsStatus);
            StartCoroutine(transition.LoadLevel(1));
        }
    }
}
