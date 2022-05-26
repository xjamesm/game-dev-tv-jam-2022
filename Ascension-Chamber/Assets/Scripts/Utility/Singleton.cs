using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            var instances = FindObjectsOfType<T>();
            int count = instances.Length;
            if (count > 0)
            {
                if (count == 1)
                    return instance = instances[0];

                Debug.LogWarning("More than one singleton of type: " + typeof(T) + "found. Destroying all but first instance.");
                for (int i = 1; i < count; i++)
                {
                    Destroy(instances[i].gameObject);
                }
                return instance = instances[0];
            }

            var newObject = new GameObject();
            instance = newObject.AddComponent<T>();
            return instance;
        }
    }
}
