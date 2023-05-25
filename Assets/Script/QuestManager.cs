using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuestManager : MonoBehaviour
{
    public GameObject questBoard;
    public float mainQ = -1;
    private static QuestManager _instance;
    public Item[] materials;
    public Item[] items;
    PlayerControl player;
   
    public string mainTxt = "";
    public string subTxt = "";
    public int sub1 = -1;
    public static QuestManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(QuestManager)) as QuestManager;

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

    public void Reward(float exp, int weapon = -1, int material = -1)
    {
      
        Level.instance.exp += exp;
        if(exp > 0)
        {
            UIManager.instance.CreateRewardText(exp, -1, -1,  Color.green);
        }
       
        
        if (Level.instance.Exp > 100)
        {
            Level.instance.Levelup();
        }
        if (weapon != -1)
        {
            Inventory.instance.AddItem(items[weapon]);
            UIManager.instance.CreateRewardText(0, weapon, -1,Color.yellow);
        }
        if (material != -1)
        {
            Inventory.instance.AddItem(materials[material]);
            UIManager.instance.CreateRewardText(0, -1, material, Color.yellow);

        }

    }
    public void CreateBoard(string name, string contest, bool NoBtn = false)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.Stop();
        StartCoroutine(Board(name, contest, NoBtn));

    }
    IEnumerator Board(string name, string contest, bool NoBtn)
    {
        //name = 퀘스트 제목 constes = 퀘스트 내용 NoBtn= 노버튼 활성화
        yield return new WaitForFixedUpdate();
        Transform canvas = GameObject.Find("Canvas").transform;
        GameObject Board = Instantiate(questBoard, canvas);
        GameObject noBTN = GameObject.Find("NoButton");
        if (NoBtn)
        {
            noBTN.SetActive(true);

        }
        else
        {
            noBTN.SetActive(false);

        }
        Board.transform.SetParent(canvas);
        Text questName = GameObject.Find("Quest Name").GetComponent<Text>();

        Text questConTent = GameObject.Find("Quest Content").GetComponent<Text>();
        questName.text = name;
        Board.name = name + " Board";
        questConTent.text = contest;
    }

    public void Tutorial1()
    {

    }
    public void Tutorial2()
    {

    }
    public void Tutorial3()
    {

    }
    public void Main1()
    {
        mainQ = 1.1f;
    }
    public void Main2()
    {
        mainQ = 2.1f;
    }
    
    public void Sub1()
    {
        sub1 = 0;
    }

}
