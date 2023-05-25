using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action: MonoBehaviour
{
    [SerializeField]

    private float range; //습득가능 거리

    [SerializeField]
    private Text actionText; //행동을 보여 줄 텍스트   ex 깡통을 열고있다

    GameObject player;
  
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

           
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
  
            ItemInfoAppear();         
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        ItemInfoDisappear();
    }


    
    private void ItemInfoAppear()
    {
        
        actionText.gameObject.SetActive(true);
        actionText.text = GetComponent<ItemPickUp>().item.itemName + "획득" +"(E)";
    }

    private void ItemInfoDisappear()
    {
        actionText.gameObject.SetActive(false);
    }

   
   
    
    
}
