using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour,IPointerClickHandler
{
    public static InventoryUI instance;
    [Range(0, 8)]
    public int horizontalCount = 8; // 세로 슬롯 개수
    [Range(0, 8)]
    public int verticalCount = 8;   // 가로 슬롯 개수
    public float slotMargin = 12; // 슬롯 상하좌우 여백

    public GameObject slotPrefab; //슬롯 프리팹

    [Range(50f, 100f)]
    public float slotSize; // 슬롯 크기

    public RectTransform slotArea; //슬롯이 위치할 영역

    public List<ItemSlot> slots; // 슬롯 리스트

    public Vector2 startPos; //슬롯 배치 시작 위치
   
    public int max;

    public GameObject description;
    public GameObject option;
   
    public void CreateSlots()
    {
        slotPrefab.TryGetComponent(out RectTransform slotRect);
        slotRect.sizeDelta = new Vector2(slotSize, slotSize);

        slotPrefab.TryGetComponent(out ItemSlot slot);
        if (slot == null)
        {
            slotPrefab.AddComponent<ItemSlot>();
        }

        slots = new List<ItemSlot>(verticalCount * horizontalCount);

        max = verticalCount * horizontalCount;
        Vector2 curPos = startPos;

        for (int i = 0; i < verticalCount; i++)
        {
            for (int j = 0; j < horizontalCount; j++)
            {               
                RectTransform rt = CloneSlot();
                rt.pivot = new Vector2(0, 1);
                rt.anchoredPosition = curPos;

                ItemSlot slotUI = rt.GetComponent<ItemSlot>();
                slotUI.index = i * verticalCount + j;
                slots.Add(slotUI);
                slotUI.name = $"ItemSlot{slotUI.index}";

                curPos.x += slotMargin + slotSize;
            }

            curPos.x = startPos.x;
            curPos.y -= (slotMargin + slotSize);
        }

    }
    private void OnEnable()
    {
        GameManager.Instance.isStop = true;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.Instance.isStop = false;
        description.SetActive(false);
        option.SetActive(false);
        Time.timeScale = 1;
    }
    private RectTransform CloneSlot()
    {
        GameObject slot = Instantiate(slotPrefab);
        RectTransform rt = slot.GetComponent<RectTransform>();
        rt.SetParent(slotArea);

        return rt;
    }

    public int SearchItem(Item item)
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
                return i;
        }

        return -1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            option.SetActive(false);
        }
    }
}
