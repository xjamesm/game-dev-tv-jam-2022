using UnityEngine;

namespace UI
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T instance = null;
        public static T Instance { get => instance; }

        protected virtual void Awake()
        {
            if (instance != null) Destroy(gameObject);
            else
                instance = (T)this;
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && instance != null)
            {
                MenuManager.Instance.OpenMenu(instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            MenuManager.Instance.CloseMenu();
        }
    }
}
