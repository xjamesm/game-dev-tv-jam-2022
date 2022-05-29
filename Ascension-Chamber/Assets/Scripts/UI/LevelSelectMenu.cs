using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LevelSelectMenu : Menu<LevelSelectMenu>
    {
        [SerializeField] GameObject levelSelectObject;

        private Transform levelSeletParent;

        protected override void Awake()
        {
            base.Awake();

            levelSeletParent = levelSelectObject.transform.parent;

            levelSelectObject.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                GameObject levelS = Instantiate(levelSelectObject, levelSeletParent);
                levelS.GetComponent<LevelSelectButton>().UpdateLevelSelect(i + 1);
                levelS.SetActive(true);
            }
        }
    } 
}
