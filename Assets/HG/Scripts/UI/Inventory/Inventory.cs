using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> items = new List<Item>();

    public Item selectedItem;
   
    public bool AddItem(Item item) // 아이템 추가
    {
        InventoryUI slot = InventoryUI.instance;

        Item sitem = ItemData.instance.Search(item);
        if (items.Count >= slot.max)
        {
            return false;
        }        

        for(int i = 0; i < slot.slots.Count; i++)
        {
            if (slot.slots[i].item == null)
            {
                slot.slots[i].Add(sitem);
                break;
            }
        }
        items.Add(sitem);
        
        return true;
    }

    public bool RemoveItem(Item item) //아이템 제거
    {
        InventoryUI slot = InventoryUI.instance;

        if (items.Count == 0)
            return false;

        if (items.Remove(item))
        {
            slot.slots[slot.SearchItem(item)].Remove();           
            return true;
        }

        return false;
    }

    public void Sort() // 아이템 정렬
    {
        InventoryUI slot = InventoryUI.instance;

        for (int i = 0; i < items.Count; i++)
        {
            slot.slots[i].Add(items[i]);
        }

        for (int i = items.Count; i < slot.slots.Count; i++)
        {
            slot.slots[i].Remove();
        }
    }

    public void Replace(Item org, Item ch) // 특정 아이템 대체하기
    {
        InventoryUI slot = InventoryUI.instance;

        if (items.Remove(org))
        {            
            slot.slots[slot.SearchItem(org)].Add(ch);
        }

        items.Add(ch);

    }

    private void OnDisable()
    {
        selectedItem = null;
    }

    public bool Contains(Item item)
    {
        if (items.Contains(item))
            return true;

        return false;
    }
}
