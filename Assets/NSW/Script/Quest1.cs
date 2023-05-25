using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using TMPro;
public class Quest1 : MonoBehaviour
{
    public Flowchart flowchart;
    float limitTIme = 90;
    public TextMeshProUGUI countTxt;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.mainQ = 1;
        QuestManager.Instance.CreateBoard("Main1", "1분 30초 이내에 연구소에서 나가자" + "\n\n보상: 경험치 100,단검", false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        int limitCnt = (int)limitTIme;
       
        countTxt.text = limitCnt.ToString();
        if (limitTIme > 0 && QuestManager.Instance.mainQ == 1.1f)
        {
            limitTIme -= Time.deltaTime;
        }
        else if (QuestManager.Instance.mainQ == 1.1f && limitTIme <= 0)
        {
           
            player.stat.curHp = player.stat.MaxHp;
            SceneManager.LoadScene("Quest1");
        }
        if(QuestManager.Instance.mainQ == 1.2f)
        {
            QuestManager.Instance.Reward(500, 0);
            QuestManager.Instance.mainQ = 1.3f;
            QuestManager.Instance.mainTxt = "";
            SceneManager.LoadScene("Quest2");
        }
    }
   
}
