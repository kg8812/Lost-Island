using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static ItemData instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public Item Search(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (item.itemName == items[i].itemName)
                return items[i];
        }

        return null;
    }
}
