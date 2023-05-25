using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : ItemSlot
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowItemInfo);
        itemImage.sprite = item.itemImage;             
    }

    private void OnEnable()
    {
        if (Inventory.instance.Contains(item))
        {
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.color = Color.black;
        }
    }
    public new void OnPointerClick(PointerEventData eventData)
    {      
       
    }

    public void ShowItemInfo()
    {
        Crafting.instance.selectedItem = item;
        Crafting.instance.itemInfo.SetActive(true);
        Crafting.instance.itemName.text = item.itemName;
        Crafting.instance.itemDescription.text = item.description;
        switch (item.itemType)
        {
            case Item.ItemType.Stuff:
                Crafting.instance.itemType.text = "재료 아이템";
                break;
            case Item.ItemType.Weapon:
                Crafting.instance.itemType.text = "장비 아이템";
                break;
        }
        Crafting.instance.itemImage.sprite = item.itemImage;

    }
}
