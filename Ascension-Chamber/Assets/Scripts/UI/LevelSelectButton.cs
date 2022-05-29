using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private TMP_Text turnsLabel;
    [SerializeField] private TMP_Text turnsNumber;
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    int level;
    Color originalbuttoncolor = Color.clear;

    public void UpdateLevelSelect(int levelNum)
    {
        levelTxt.text = "Level " + levelNum;
        level = levelNum;

        if (originalbuttoncolor == Color.clear)
            originalbuttoncolor = buttonImage.color;

        if (LevelManager.Instance.IsLevelUnlocked(levelNum))
        {
            int turns = LevelManager.Instance.GetLevelTurnsBest(levelNum);
            if(turns != -1)
            {
                turnsLabel.text = "Turns:";
                turnsNumber.text = "" + turns;
            }
            else
            {
                turnsLabel.text = "";
                turnsNumber.text = "";
            }

            button.interactable = true;
            buttonImage.color = originalbuttoncolor;
        }
        else
        {
            turnsLabel.text = "";
            turnsNumber.text = "";

            if (levelNum == 1)
            {
                button.interactable = true;
                buttonImage.color = originalbuttoncolor;
                return;
            }
        

            button.interactable = false;
            buttonImage.color = new Color(buttonImage.color.r * 0.2f, buttonImage.color.g * 0.2f, buttonImage.color.b * 0.2f, buttonImage.color.a);
        }
    }

    public void OnClick()
    {
        LevelManager.Instance.LoadLevel(level);
    }
}

