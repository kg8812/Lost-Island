using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{   
    public enum ItemType // 아이템 유형
    {
        Weapon,   //무기
        Stuff,    //재료
    }

    public string itemName; // 아이템 이름
    public ItemType itemType; // 아이템 타입
    public Sprite itemImage; // 아이템 이미지 

    public List<Item> recipe = new List<Item>();  
    
    [TextArea]
    public string description; //아이템 설명
    public ItemSlot slot; //현재 위치한 슬롯
    public void Use()
    {
        TryGetComponent(out IOnItemUse use);

        if (use != null)
        {
            use.OnItemUse();
        }
    }
}
