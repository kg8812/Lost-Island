using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmItem : MonoBehaviour
{
    public Item item;

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {          
            if (Inventory.instance.AddItem(item))
            {
                Destroy(gameObject);
            }           
            
        }        
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Interaction();
        }
    }

}
