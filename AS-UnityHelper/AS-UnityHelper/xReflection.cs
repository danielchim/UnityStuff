using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AS_UnityHelper
{
    public class xReflection
    {
        public static string GetAssemblyPath(string name)
        {
            return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\" + name + ".dll";
        }

        // Wrapper for FindObjectsOfType
        // returns array list of unity object type
        // (make sure the type you are looking for has active objects in the scene)
        public static T[] getArrayListOfType<T>() where T : UnityEngine.Object
        {
            return UnityEngine.Object.FindObjectsOfType<T>();
        }

        public static FieldInfo GetFieldInfo<T>(string fieldName)
        {
            FieldInfo fieldInfo = null;
            try
            {
                fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Instance 
                    | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (fieldInfo == null)
                    Debug.LogWarning(string.Format("Couldn't find field {0} in type {1}", fieldName, typeof(T).Name));

            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            return fieldInfo;
        }

        public static object GetFieldValue<T>(object thisObj, string fieldName)
        {
            if (thisObj == null)
                Debug.LogWarning(string.Format("Object of type {0} is null", typeof(T)));

            FieldInfo fieldInfo = GetFieldInfo<T>(fieldName);

            if (fieldInfo == null) 
                Debug.LogWarning(string.Format("Couldn't find field {0} in type {1}", fieldName, typeof(T).Name));

            return fieldInfo.GetValue(thisObj);
        }

        public static void SetFieldValue<T>(object thisObj, string fieldName, object val)
        {
            if (thisObj == null)
            {
                Debug.LogWarning(string.Format("Object of type {0} is null", typeof(T)));
                return;
            }

            FieldInfo fieldInfo = GetFieldInfo<T>(fieldName);
            if (fieldInfo == null)
            {
                Debug.LogWarning(string.Format("Couldn't find field {0} in type {1}", fieldName, typeof(T).Name));
                return;
            }

            try
            {
                fieldInfo.SetValue(thisObj, val);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public static MethodInfo GetMethodInfo<T>(string methodName)
        {
            MethodInfo methodInfo = null;
            try
            {
                methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Instance
                    | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (methodInfo == null)
                    Debug.LogWarning(string.Format("Couldn't find method {0} in type {1}", methodName, typeof(T).Name));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            return methodInfo;
        }

        public static void InvokeMethod<T>(bool hasInstance, object obj, 
            string methodName, params object[] methodParams)
        {
            try
            {
                GetMethodInfo<T>(methodName).Invoke(hasInstance ? obj : Activator.CreateInstance(typeof(T)), methodParams);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            PropertyInfo propertyInfo = null;
            try
            {
                propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Instance
                    | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (propertyInfo == null)
                    Debug.LogWarning(string.Format("Couldn't find method {0} in type {1}", 
                        propertyName, typeof(T).Name));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            return propertyInfo;
        }

        public static bool ArrayContains(Array array, object obj)
        {
            return Array.IndexOf(array, obj) >= 0;
        }

        public static bool CaseInsensitiveContains(string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
        public static float RandomNumInRange(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}
