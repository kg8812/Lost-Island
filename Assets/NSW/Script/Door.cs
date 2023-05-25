using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
public class Door : MonoBehaviour
{
    public Flowchart flowchart;
    private void Start()
    {
        if (QuestManager.Instance.mainQ >= 0.3f)
            flowchart.SetBooleanVariable("Tutorial", false);
        else
            flowchart.SetBooleanVariable("Tutorial", true);

    }
    public void ToMain()
    {
        if(QuestManager.Instance.mainQ == 0.3f)
        {
            SceneManager.LoadScene("Quest1");
        }
        else if(QuestManager.Instance.mainQ == 2.3f)
        {
            SceneManager.LoadScene("Start");
        }
        else if(QuestManager.Instance.mainQ == 3)
        {
            SceneManager.LoadScene("Test");
        }
    }
    public void ToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    
    }
}
