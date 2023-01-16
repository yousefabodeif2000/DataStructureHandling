using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
    protected static T _instance = default;
    public static T Instance => _instance;

    public bool dontDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = GetComponent<T>();
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
