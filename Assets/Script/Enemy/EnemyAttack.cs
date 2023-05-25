using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy enemy;
    BoxCollider boxCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boxCollider = GetComponent<BoxCollider>();
            other.GetComponent<Player>().OnHit(enemy.atk);
            boxCollider.enabled = false;
        }
    }
}
