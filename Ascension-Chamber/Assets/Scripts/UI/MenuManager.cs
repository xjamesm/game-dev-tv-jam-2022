using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace UI
{
    public class MenuManager : Singleton<MenuManager>
    {
        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private LevelSelectMenu levelSelectPrefab;
        [SerializeField] private OptionsMenu optionsMenuPrefab;

        [SerializeField] private Transform menuParent;

        private Stack<Menu> menuStack = new Stack<Menu>();

        private void Awake()
        {
            InitMenus();
        }

        private void InitMenus()
        {
            if (menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                menuParent = menuParentObject.transform;
            }

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = GetType().GetFields(flags);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;

                if (prefab != null)
                {
                    Menu instance = Instantiate(prefab, menuParent);
                    if (prefab != mainMenuPrefab)
                    {
                        instance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(instance);
                    }
                }
            }
        }

        public void OpenMenu(Menu instance)
        {
            if (instance == null)
            {
                Debug.LogWarning("Trying to open a menu without an instance");
                return;
            }

            if (menuStack.Count > 0)
            {
                foreach (Menu menu in menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            instance.gameObject.SetActive(true);
            menuStack.Push(instance);
        }

        public void CloseMenu()
        {
            if (menuStack.Count == 0)
            {
                Debug.LogWarning("No menus left in stack");
                return;
            }

            Menu instance = menuStack.Pop();
            instance.gameObject.SetActive(false);

            if (menuStack.Count > 0)
            {
                Menu nextMenu = menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }
}
