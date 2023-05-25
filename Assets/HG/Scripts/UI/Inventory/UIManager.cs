using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inven;
    public GameObject craft;
    public GameObject dmgText;
    public GameObject questBoard;
    public GameObject equipment;
    public GameObject rewardText;
    public static UIManager instance;

    public Image status;
    private void Awake()
    {
        instance = this;
        Inventory.instance = inven.GetComponent<Inventory>();
        InventoryUI.instance = inven.GetComponent<InventoryUI>();
        Crafting.instance = craft.GetComponent<Crafting>();
        Status.instance = status.GetComponent<Status>();
        inven.GetComponent<InventoryUI>().CreateSlots();
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && (!GameManager.Instance.isStop || inven.activeSelf))
        {
            inven.SetActive(!inven.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.U) && (!GameManager.Instance.isStop || equipment.activeSelf))
        {
            equipment.SetActive(!equipment.activeSelf);
        }

        OnImage();
    }

    public void Close()
    {
        inven.SetActive(false);
    }
    public IEnumerator RewardText(float exp, int weapon, int material, Color color)
    {
        Transform pos = GameObject.FindGameObjectWithTag("Player").transform;
        if (pos != null)
        {
           
            if (exp != 0)
            {
                GameObject text = Instantiate(dmgText);
                text.transform.position = pos.position + Vector3.up;
                text.GetComponent<TextMeshPro>().color = color;
                text.GetComponent<TextMeshPro>().text = "EXP + " + Mathf.Ceil(exp).ToString();
            }
            else if (weapon != -1)
            {
                
                yield return new WaitForSeconds(1);
                if (pos != null)
                {
                    GameObject text = Instantiate(dmgText);
                    text.transform.position = pos.position + Vector3.up;
                    text.GetComponent<TextMeshPro>().color = color;
                    Item reward = QuestManager.Instance.items[weapon];
                    text.GetComponent<TextMeshPro>().text = reward.itemName + "을 획득하셨습니다.";
                }
            }
            else if (material != -1)
            {
                yield return new WaitForSeconds(2);
                if(pos != null)
                {
                    GameObject text = Instantiate(dmgText);
                    text.transform.position = pos.position + Vector3.up;
                    text.GetComponent<TextMeshPro>().color = color;
                    Item reward = QuestManager.Instance.materials[material];
                    text.GetComponent<TextMeshPro>().text = reward.itemName + "을 획득하셨습니다.";
                }
               
            }
        }
        

    }
    public void CreateRewardText(float exp, int weapon, int material, Color color)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        StartCoroutine(RewardText(exp, weapon, material, color));

    }
    public void CreateDmgText(float dmg, Transform pos, Color color, bool isMiss = false)
    {
        GameObject text = Instantiate(dmgText);

        text.transform.position = pos.position + Vector3.up;
        text.GetComponent<TextMeshPro>().color = color;
        if (isMiss)
        {
            text.GetComponent<TextMeshPro>().text = "Miss";
        }
        else
        {
            text.GetComponent<TextMeshPro>().text = Mathf.Ceil(dmg).ToString();

        }
    }

    void OnImage()
    {
        if (Input.GetKeyDown(KeyCode.S) && (!GameManager.Instance.isStop || status.gameObject.activeSelf))
        {
            status.gameObject.SetActive(!status.gameObject.activeSelf);

        }

    }
}
