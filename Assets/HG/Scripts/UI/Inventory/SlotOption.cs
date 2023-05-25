using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotOption : MonoBehaviour
{
    public GameObject dropOption;
    public Text useText;

    public void Close()
    {
        gameObject.SetActive(false);
        dropOption.SetActive(false);
    }

    public void Use()
    {
        Inventory.instance.selectedItem.Use();
        Close();
    }   

    public void DropOption()
    {
        dropOption.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Drop()
    {
        Inventory.instance.RemoveItem(Inventory.instance.selectedItem);
        Close();
    }
}
