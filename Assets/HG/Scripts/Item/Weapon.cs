using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IOnItemUse
{ 
    public WeaponInfo info;
    public Transform firePos;
    public bool isLeft;
    Player player;  

    public void OnItemUse()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Weapon wp = player.EquipWeapon(this);

        if (wp != null)
        {
            Inventory.instance.Replace(this,wp);
        }
        else
            Inventory.instance.RemoveItem(this);

    }
 
}
