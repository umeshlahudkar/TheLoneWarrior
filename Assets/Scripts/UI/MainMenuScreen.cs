using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{

    [SerializeField] private SettingScreen settingScreen;

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingButtonClick()
    {
        settingScreen.gameObject.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
