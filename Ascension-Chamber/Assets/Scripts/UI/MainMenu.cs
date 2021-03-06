using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : Menu<MainMenu>
    {
        public void OnStartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void PressOptions()
        {
            OptionsMenu.Open();
        }

        public void PressLevelSelect()
        {
            LevelSelectMenu.Open();
        }
    } 
}
