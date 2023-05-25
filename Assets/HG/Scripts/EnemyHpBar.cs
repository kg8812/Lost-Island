using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    Enemy enemy;
    public Image redBar;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }   
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + Vector3.up * 2);
        redBar.fillAmount = enemy.hp / enemy.maxHp;
    }
}
