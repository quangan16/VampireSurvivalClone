using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public  static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }

            return _instance;
        }
        
    }

    public virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.GameObject());
        }
        else
        {
            print(typeof(T).Name);
            _instance = this as T;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
        Destroy(gameObject);
    }
}


public class PersistedSingleton<T> : Singleton<T> where T: Component
{
    public override void Awake()
    {
        base.Awake(); 
        DontDestroyOnLoad(this.GameObject());
        
            
       
    }
}