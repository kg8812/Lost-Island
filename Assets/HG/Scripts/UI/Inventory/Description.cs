using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public Image itemImage;
    public Text descriptionText;
    public Text itemType;
    public Text itemName;
    
    private void OnEnable()
    {
        Item item = Inventory.instance.selectedItem;

        itemImage.sprite = item.itemImage;
        descriptionText.text = item.description;
        switch(item.itemType)
        {
            case Item.ItemType.Stuff:
                itemType.text = "재료 아이템";
                break;
            case Item.ItemType.Weapon:
                itemType.text = "장비 아이템";
                break;
        }
        itemName.text = item.itemName;
        
    }

}
