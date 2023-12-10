using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonComponent<T>: MonoBehaviour where T : Component
{ 
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
    }
}
