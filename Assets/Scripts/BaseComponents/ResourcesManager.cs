using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesManager
{
    private static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    private static string[] _resourcesPatches = new[] {"", "WindowViews/", "PopupWindowViews/", "PartsView/" };

    static GameObject getPrefab(string name)
    {
        if (!prefabs.ContainsKey(name))
        {
            prefabs[name] = Resources.Load<GameObject>(name);
            if (prefabs[name] == null)
            {
                foreach (var dir in _resourcesPatches)
                {
                    prefabs[name] = Resources.Load<GameObject>(dir + name);

                    if (prefabs[name] != null)
                        break;
                }
            }
        }
        return prefabs[name];
    }


    public static T InstantiatePrefab<T>(Transform parent = null)
    {
        string name = typeof(T).Name;

        try
        {
            var gameObject = MonoBehaviour.Instantiate(getPrefab(name), parent);
            var neededComponent = gameObject.GetComponent<T>();

            if (neededComponent == null)
            {
                string txt = $"нет такого компонента/объекта  {name} {typeof(T)}";
                Debug.LogWarning(txt);
                throw new NullReferenceException(txt);
            }

            return neededComponent;
        }
        catch (Exception e)
        {
            Debug.LogError("Error in InstantiatePrefabByType");
            Debug.LogError($"name is {name}");
            Debug.Log(e.Message);

            return default(T);
        }


    }

    public ScriptableObject InstantiateScriptableObjectByName<T>(string name) where T : ScriptableObject
    {
        var gameObject = Object.Instantiate(Resources.Load<T>(name));

        if (gameObject == null)
        {
            string txt = $"нет такого компонента/объекта  {name} {typeof(T)}";
            Debug.LogWarning(txt);
            throw new NullReferenceException(txt);
        }


        return gameObject;
    }

}
