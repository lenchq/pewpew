using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // public AssetBundle assetBundle;
    // public string[] scenePaths;
    
    private GameObject _exitButtonObj;
    private GameObject _playButtonObj;
    private GameObject _backButtonObj;
    private GameObject _sideMenu;
    private GameObject _levelSelector;
    
    private bool _levelSelectorOpened = false;
    

    public static MainMenuManager Current;

    void Awake()
    {
        // assetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        // scenePaths = assetBundle.GetAllScenePaths();
        
        _exitButtonObj = GameObject.Find("ExitBtn");
        _playButtonObj = GameObject.Find("PlayBtn");
        _backButtonObj = GameObject.Find("BackBtn");
        _sideMenu = GameObject.Find("SidePanel");
        _levelSelector = GameObject.Find("LevelSelect");
        
        _levelSelector.SetActive(false);

        _playButtonObj.GetComponent<Button>().onClick.AddListener(ToggleLevelSelector);
        _exitButtonObj.GetComponent<Button>().onClick.AddListener(ExitApp);
        _backButtonObj.GetComponent<Button>().onClick.AddListener(ToggleLevelSelector);

        Current = this;
    }

    private void ExitApp()
    {
        Application.Quit(0);
    }

    private void ToggleLevelSelector()
    {
        if (_levelSelectorOpened)
        {
            _levelSelector.SetActive(false);
            _sideMenu.SetActive(true);
        }
        else
        {
            _levelSelector.SetActive(true);
            _sideMenu.SetActive(false);
        }

        _levelSelectorOpened = !_levelSelectorOpened;

    }
}
