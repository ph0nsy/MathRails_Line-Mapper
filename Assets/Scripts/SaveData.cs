using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveData : MonoBehaviour
{

    public static SaveData instance;

    public bool firstLoad = false;

    private string dataDirPath;
    
    [SerializeField]
    private string fileName = "save.data";
    [System.Serializable]
    public struct levelInfo{   
        public string levelName;
        public string[] mathFunction_A;
        public string[] mathFunction_B;
        public string[] posibilities_A;
        public string[] posibilities_B;
        public int[] solutions_A;
        public int[] solutions_B;
    };
    
    public Dictionary<string, List<int>> levelsStatus = new Dictionary<string, List<int>>()
    {
        {"unlocked", new List<int>(25)},
        {"graded", new List<int>(25)},
        {"gradeGiven", new List<int>(25)}
    }; 

    public List<levelInfo> levelsInfo;
    public int currLvl = 0;
    // Update is called once per frame
    public void Save(Dictionary<string, List<int>> data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataDirPath));
            string dataToStore = JsonConvert.SerializeObject(data);
            Debug.Log(dataToStore);
            using (FileStream stream = new FileStream(dataDirPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) { Debug.LogError("Error ocurred on saving at: " + dataDirPath + " - " + e); }
    }

    public Dictionary<string, List<int>> Load()
    {
        Dictionary<string, List<int>> loadedData = null;
        if(File.Exists(dataDirPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(dataDirPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(dataToLoad);
                Debug.Log(dataToLoad);
            }
            catch (Exception e) { Debug.LogError("Error ocurred on loading at: " + dataDirPath + " - " + e); }
        }
        return loadedData;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Manager").Length > 1){
            Destroy(this.gameObject);
            return;
        }
        Screen.SetResolution(Screen.resolutions[^1].width, Screen.resolutions[^1].height, true);
        if(PlayerPrefs.HasKey("Fullscreen"))
            Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") > 0 ? true : false;
        else
            PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        instance = this;
        firstLoad = true;
        DontDestroyOnLoad(this.gameObject);
        dataDirPath = Path.Combine(Application.persistentDataPath, fileName);
        if(File.Exists(dataDirPath))
        {
            Dictionary<string, List<int>> loadedData = Load();
            levelsStatus = loadedData;
        } 
        else 
        {
            levelsStatus["unlocked"].Add(1); 
            for(int i = 0; i < 24;i++){    // Create a list of No. of level size
                levelsStatus["unlocked"].Add(0); 
                levelsStatus["graded"].Add(0); 
                levelsStatus["gradeGiven"].Add(0); 
            }
            Save(levelsStatus);
        }
    }

    void OnGUI(){
        GUILayout.Label(Screen.fullScreen.ToString());
        GUILayout.Label(Screen.currentResolution.ToString());
        GUILayout.Label(Screen.width.ToString() + " x " + Screen.height.ToString());
        GUILayout.Label(GameObject.FindGameObjectsWithTag("Manager").Length.ToString());
        GUILayout.Label((PlayerPrefs.GetInt("Fullscreen") > 0 ? true : false).ToString());
    }
}
