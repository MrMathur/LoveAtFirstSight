using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class PlayerStats
{
    public static List<LevelDetails> Levels { get; set; }
}

public struct LevelDetails {
    public bool cleared;
    public int levelIndex;

    //Constructor (not necessary, but helpful)
    public LevelDetails(int levelIndex, bool cleared) {
        this.cleared = cleared;
        this.levelIndex = levelIndex;
    }
}



public class GameSetup : MonoBehaviour
{

    void Start()
    {
        if (PlayerStats.Levels == null) {
            PlayerStats.Levels = new List<LevelDetails>();
            for (int i=0; i< 15; i++){
                LevelDetails x = new LevelDetails(i,false);
                PlayerStats.Levels.Add(x);
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
    }
}