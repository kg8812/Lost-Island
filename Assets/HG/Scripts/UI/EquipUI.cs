using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public static EquipUI instance;
    public Image weaponImage;
    public Text description;
    public GameObject info;
    public GameObject noWeapon;
    public GameObject noSlot;
    public Player player;
    Weapon curWeapon;
    
    private void OnEnable()
    {
        GameManager.Instance.isStop = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Time.timeScale = 0;
        curWeapon = player.stat.currentWeapon;
        noSlot.SetActive(false);

        ResetUI();
    }

    void ResetUI()
    {
        if (curWeapon == null)
        {
            info.SetActive(false);
            noWeapon.SetActive(true);
        }
        else
        {
            info.SetActive(true);
            noWeapon.SetActive(false);
            SetInfo();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.isStop = false;
        Time.timeScale = 1;
    }

    void SetInfo()
    {
        weaponImage.sprite = curWeapon.itemImage;
        description.text = curWeapon.description;
    }
    
    public void UnEquip()
    {
        if (curWeapon != null)
        {
            if (Inventory.instance.AddItem(curWeapon))
            {
                curWeapon = null;
                player.UnEquipWeapon();
                ResetUI();
            }
            else
            {
                noSlot.SetActive(true);
                StartCoroutine(Disable());
            }
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSecondsRealtime(1);
        noSlot.SetActive(false);
    }
}
