using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private SettingSystem setting;
    [SerializeField] private GameObject prefab;
    [SerializeField] private VideoPlayer video;

    public void Start()
    {
        AudioSystem.Instance.PlayOnce("MenuBackground");
        Init();
    }

    public void Init()
    {
        video.url = Path.Combine(Application.streamingAssetsPath, "Video/Waterfall.mp4");
    }
    public void StartGame()
    {
        AudioSystem.Instance.StopAll();
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenSetting()
    {
        setting.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
   
    
   
}

