using System;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xLoader
    {
        // the class base type must be MonoBehaviour
        public static void Load<T>() where T : MonoBehaviour
        {
            if (GameObject.Find("X" + typeof(T).Name) == null)
            {
                GameObject myObj = new GameObject("X" + typeof(T).Name);
                myObj.AddComponent(typeof(T));
                UnityEngine.Object.DontDestroyOnLoad(myObj);
            }
            else
            {
                Debug.Log("Already Loaded");
            }
        }

        // the class base type must be MonoBehaviour
        public static void Unload<T>() where T : MonoBehaviour
        {
            if (GameObject.Find("X" + typeof(T).Name) != null)
            {
                UnityEngine.Object.Destroy(GameObject.Find("X" + typeof(T).Name));
            }
            else
            {
                Debug.Log("Doesn't exist");
            }
        }
    }
}
