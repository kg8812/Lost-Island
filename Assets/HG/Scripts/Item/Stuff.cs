using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : Item,IOnItemUse
{
    public void OnItemUse()
    {
        UIManager.instance.inven.SetActive(false);

        UIManager.instance.craft.SetActive(true);
    }
}
