using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject startUI;

    public void OpenStartUI()
    {
        startUI.SetActive(true);
    }
    public void CloseStartUI()
    {
        startUI.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
