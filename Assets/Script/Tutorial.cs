using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    Transform player;
    Vector3 oriPos;
    public GameObject zombiePrf;
    GameObject zombie;
    public Item item;
    public Transform zombiePos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        oriPos = player.position;
        QuestManager.Instance.CreateBoard("Tutorial1", "이동하고싶은곳을 마우스 오른쪽클릭하면 이동합니다.\n" +
            "멀리 이동해보세요" + "\n\n보상: 경험치 50" ,false);

        QuestManager.Instance.mainQ = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (QuestManager.Instance.mainQ == 0)
        {
            float dist = Vector3.Distance(player.transform.position, oriPos);
            if (dist > 15)
            {
                QuestManager.Instance.Reward(50.0f,0,0);
                QuestManager.Instance.mainTxt = "";
                QuestManager.Instance.CreateBoard("Tutorial2", "잘하셨습니다!\n인벤토리에서 아이템을 장착할 수 있습니다.\n" +
                   "i를 눌러 인벤토리를 열고 단검을 장착합시다." + "\n\n보상: 경험치 100");
                QuestManager.Instance.mainQ = 0.1f;
                
            }
        }
        if (QuestManager.Instance.mainQ == 0.1f)
        {
            if(player.gameObject.GetComponent<Player>().stat.currentWeapon != null)
            {
                if (player.gameObject.GetComponent<Player>().stat.currentWeapon.info.Type == WeaponInfo.TYPE.Knife)
                {
                    QuestManager.Instance.Reward(100.0f);
                    QuestManager.Instance.mainTxt = "";

                    QuestManager.Instance.CreateBoard("Tutorial3", "잘하셨습니다!\n적을 마우스 오른쪽으로 " +
                        "클릭하면 자동으로 공격합니다\n" +
                        "좀비를 쓰러트려 봅시다" + "\n\n보상: 경험치 50,스프링");
                    if (zombie == null)
                    {
                        zombie = Instantiate(zombiePrf, zombiePos);
                        zombie.GetComponent<Enemy>().isOffensive = true;
                        zombie.GetComponent<Enemy>().traceRange = 9999;
                    }
                    QuestManager.Instance.mainQ = 0.2f;
                }
            }
           
        }
        if (QuestManager.Instance.mainQ == 0.2f)
        {
           
            if(zombie == null)
            {
                QuestManager.Instance.Reward(50.0f,-1,1);
                QuestManager.Instance.mainTxt = "";
                QuestManager.Instance.mainQ = 0.3f;
                SceneManager.LoadScene("House");
            }
           

        }
    }
}
