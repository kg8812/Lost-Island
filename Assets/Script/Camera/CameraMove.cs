using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject player;
    Vector3 campos = new Vector3(7, 10f, -5);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");      

    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        transform.position = player.transform.position + campos;
    }
}
