using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public static string LevelPrefix = "Level_";
    public static string LevelClearPrefix = "LevelClear_";

    public void WinLevel(int levelnum, int turns)
    {
        PlayerPrefs.SetInt(LevelClearPrefix + levelnum, turns);
    }

    public bool IsLevelUnlocked(int levelnum)
    {
        if (PlayerPrefs.HasKey(LevelClearPrefix + levelnum))
            return PlayerPrefs.GetInt(LevelClearPrefix + levelnum) != 0;

        return false;
    }

    public int GetLevelTurnsBest(int levelnum)
    {
        if (PlayerPrefs.HasKey(LevelClearPrefix + levelnum))
            return PlayerPrefs.GetInt(LevelClearPrefix + levelnum);

        return -1;
    }

    public void LoadLevel(int levelnum)
    {
        SceneManager.LoadScene(LevelPrefix + levelnum);
    }
}
