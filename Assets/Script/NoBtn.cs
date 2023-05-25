using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBtn : MonoBehaviour
{
    public GameObject board;
    PlayerControl player;
    public void CloseBoard()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.Move();
        Destroy(board);
    }
}
