using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pausedUI;
    bool isPaused = false;
    private static GameManager _instance;
    GameObject pausedui;
    public Player player;
    public bool isStop = false;
    GameObject flowchart;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
   
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
           
            flowchart = GameObject.FindGameObjectWithTag("MainText");
                
            if (flowchart != null)
            {
                flowchart.SetActive(false);
                player.gameObject.GetComponent<PlayerControl>().Move();
            }
        }
    }
    
    public void PauseGame()
    {
        if (pausedui == null)
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            pausedui = Instantiate(pausedUI,canvas);
            pausedui.transform.SetParent(canvas);
        }
        else
        {
            pausedui.SetActive(true);
        }
        isPaused = true;

        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        if (pausedui != null)
            pausedui.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

    }
}
