using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text levelName;

    private void Start()
    {
        levelName.text = "Level " + gameManager.LevelNumber;
    }
}
