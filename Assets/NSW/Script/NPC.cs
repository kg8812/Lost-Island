using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPC : MonoBehaviour
{
    
    public Flowchart flowchart;
    Transform player;
    float dist;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   


    // Update is called once per frame

    public void StartBlock(string name)
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist < 5)
        {
            if(this.name != "Bed" && this.name != "Door")
            transform.LookAt(player);
            if (this.name == "Enma") transform.Rotate(new Vector3(-90, 0, 0));
            flowchart.ExecuteBlock(name);

        }
    }
}
