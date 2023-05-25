using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Aiden : MonoBehaviour
{
    Transform player;
    public Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if(dist <= 10 && Input.GetKeyDown(KeyCode.F))
        {
            
            flowchart.ExecuteBlock("에이든 대화");
        }
    }
}
