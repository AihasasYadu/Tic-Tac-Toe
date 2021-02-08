using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string name;
    public int score;
}

[System.Serializable]
public class PlayerList
{
    public Player[] player = new Player[3];
}

public class JSonManager : MonoSingletonGeneric<JSonManager>
{
    public TextAsset txtJson;
    private PlayerList highScoreList = new PlayerList();
    public PlayerList GetSaveData { get {
                                            highScoreList = JsonUtility.FromJson<PlayerList>(txtJson.text);
                                            return highScoreList;
                                        } }
    public PlayerList SetSaveData { set { 
                                            highScoreList = value;
                                            SaveData();
                                        } }

    private void SaveData()
    {
        string dataToSave = JsonUtility.ToJson(highScoreList);
        //File.WriteAllText(Application.persistentDataPath + "/HighScores.txt", dataToSave);
        File.WriteAllText("D:/Unity Projects (2020)/Tic-Tac-Toe/Assets/JSONs/HighScores.json", dataToSave);
    }
}
