using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Text loadingText;
    AsyncOperation asc;

    void Start()
    {
        asc = SceneManager.LoadSceneAsync("House");
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {      

        loadingText.text = $"{asc.progress*100}%";
    }
}
