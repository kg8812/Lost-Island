using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int index;   // 인덱스
    public Item item;   // 현재 슬롯의 아이템
    public Image itemImage; // 아이템 이미지

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            InventoryUI.instance.description.transform.position = Input.mousePosition + new Vector3(10, -10, 0);
        }

    }
    public void Add(Item item)
    {
        this.item = item;

        if (item != null)
        {
            item.slot = this;
            itemImage.sprite = item.itemImage;
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }

    public void Remove()
    {
        item = null;
        itemImage.gameObject.SetActive(false);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryUI.instance.option.SetActive(false);
        }

            if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                Inventory.instance.selectedItem = item;
                InventoryUI.instance.description.SetActive(false);
                InventoryUI.instance.option.SetActive(true);
                InventoryUI.instance.option.transform.position = Input.mousePosition + new Vector3(10, -10, 0);

                switch (Inventory.instance.selectedItem.itemType)
                {
                    case Item.ItemType.Stuff:
                        InventoryUI.instance.option.GetComponent<SlotOption>().useText.text = "제작";
                        break;
                    case Item.ItemType.Weapon:
                        InventoryUI.instance.option.GetComponent<SlotOption>().useText.text = "장착";
                        break;
                }
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Inventory.instance.selectedItem = item;

            if (!InventoryUI.instance.option.activeSelf)
            {
                InventoryUI.instance.description.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUI.instance.description.SetActive(false);
    }
}
