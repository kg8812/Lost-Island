using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable/Weapon")]
public class WeaponInfo : ScriptableObject
{
    public Vector3 pos;
    public Vector3 rot;

    public float range;
    public float atkSpeed;
    public float atk;

    [Range(0,100)]
    public float critProb;
    public float critDmg;      
    
    public enum TYPE
    {
        Rifle=0,
        Pistol,
        Sword,
        Knife
    };

    public TYPE Type;

}
