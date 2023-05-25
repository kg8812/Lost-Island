using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public static Crafting instance;
    public GameObject itemInfo;
    public Image itemImage;
    public Text itemName;
    public Text itemDescription;
    public Text itemType;

    public GameObject daggerCraft;
    public GameObject swordCraft;
    public GameObject pistolCraft;
    public GameObject rifleCraft;

    public GameObject result;

    public Item selectedItem;

    GameObject currentPage;

    bool isCraft = false;
   
    private void Start()
    {
        currentPage = daggerCraft;
        currentPage.SetActive(true);
    }
    private void OnEnable()
    {
        GameManager.Instance.isStop= true;
        CloseResult();
        itemInfo.SetActive(false);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        GameManager.Instance.isStop= false;
        Time.timeScale = 1;

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void DaggerCraft()
    {
        currentPage.SetActive(false);
        currentPage = daggerCraft;
        currentPage.SetActive(true);

    }
    public void SwordCraft()
    {
        currentPage.SetActive(false);
        currentPage = swordCraft;
        currentPage.SetActive(true);
    }
    public void PistolCraft()
    {
        currentPage.SetActive(false);
        currentPage = pistolCraft;
        currentPage.SetActive(true);

    }
    public void RifleCraft()
    {
        currentPage.SetActive(false);
        currentPage = rifleCraft;
        currentPage.SetActive(true);
    }

    
    public void Craft()
    {
        if (!isCraft)
        {
            if (selectedItem.recipe.Count == 0)
            {
                result.GetComponentInChildren<Text>().color = Color.red;

                result.GetComponentInChildren<Text>().text = "제작할 수 없는 아이템입니다!";
                result.SetActive(true);
                isCraft = true;
                StartCoroutine(corutineClose());
                return;
            }

            for (int i = 0; i < selectedItem.recipe.Count; i++)
            {
                if (!Inventory.instance.Contains(selectedItem.recipe[i]))
                {
                    result.GetComponentInChildren<Text>().color = Color.red;

                    result.GetComponentInChildren<Text>().text = "재료가 부족합니다!";
                    result.SetActive(true);
                    isCraft = true;
                    StartCoroutine(corutineClose());
                    return;
                }
            }

            for (int i = 0; i < selectedItem.recipe.Count; i++)
            {
                Inventory.instance.RemoveItem(selectedItem.recipe[i]);
            }

            Inventory.instance.AddItem(selectedItem);

            result.GetComponentInChildren<Text>().text = "제작에 성공하였습니다!";
            result.GetComponentInChildren<Text>().color = Color.green;
            result.SetActive(true);
            isCraft = true;
            StartCoroutine(corutineClose());

            Refresh();
        }
    }

    void Refresh()
    {
        currentPage.SetActive(false);
        currentPage.SetActive(true);
    }
    IEnumerator corutineClose()
    {
        yield return new WaitForSecondsRealtime(1f);
        CloseResult();
    }
    void CloseResult()
    {
        result.SetActive(false);
        isCraft = false;
    }
}
