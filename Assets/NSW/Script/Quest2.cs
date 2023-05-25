using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class Quest2 : MonoBehaviour
{
    public Player player;
    public GameObject enemy1;
    public GameObject enemy2;
    public Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.mainQ = 2;
        QuestManager.Instance.CreateBoard("Main2", "은신처를 찾자" + "\n\n보상: 경험치 800,숙련자의 단검", false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (QuestManager.Instance.mainQ == 2.1f)
        {
            if (enemy1 != null)
            {
                enemy1.GetComponent<Enemy>().isOffensive = true;
                enemy1.GetComponent<Enemy>().traceRange = 9999;

            }
            if (enemy2 != null)
            {
                enemy2.GetComponent<Enemy>().isOffensive = true;
                enemy2.GetComponent<Enemy>().traceRange = 9999;
            }

        }
        else if (QuestManager.Instance.mainQ == 2.2f)
        {
            if (enemy1 != null)
                enemy1.SetActive(false);
            if (enemy2 != null)
                enemy2.SetActive(false);
            QuestManager.Instance.Reward(800, 1);
            QuestManager.Instance.mainTxt = "";
            QuestManager.Instance.mainQ = 2.3f;
            flowchart.ExecuteBlock("Clear");


        }
    }
    public void Clear()
    {
        SceneManager.LoadScene("Hideout");
    }
}
