using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private Button _btn;
    private TextMeshProUGUI _levelText;
    private int _levelNum;

    //[SerializeField]
    private string _levelName = "";
    void Awake()
    {
        _levelName = this.name;
        _btn = GetComponentInChildren<Button>();
        _levelText = GetComponentInChildren<TextMeshProUGUI>();

        _levelNum = int.Parse(_levelName.Replace("Level", ""));
        _levelText.SetText(_levelNum.ToString());

        _btn.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        // string sceneName = MainMenuManager.Current.scenePaths[_levelNum];
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_levelName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
