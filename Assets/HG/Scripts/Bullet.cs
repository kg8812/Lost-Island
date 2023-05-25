using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject target;
    Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
        if (target != null)
            transform.LookAt(target.transform.position + new Vector3(0, 1, 0));
    }
    
    void Update()
    {       
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {           
            other.GetComponent<Enemy>().OnHit(player.stat.AtkDmg);           
            Destroy(gameObject);
        }
    }
}
