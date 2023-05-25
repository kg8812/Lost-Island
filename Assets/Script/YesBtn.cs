using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YesBtn : MonoBehaviour
{
    Text questName;
    public GameObject board;
    PlayerControl player;
    public GameObject noBtn;
    public void AcceptQuest()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.Move();
        Text questName = GameObject.Find("Quest Name").GetComponent<Text>();
        if (noBtn.activeInHierarchy)
        {
            QuestManager.Instance.subTxt = questName.text;
        }
        else
        {
            QuestManager.Instance.mainTxt = questName.text;
        }
        questName = GameObject.Find("Quest Name").GetComponent<Text>();
        QuestManager.Instance.SendMessage(questName.text,SendMessageOptions.DontRequireReceiver);
        Destroy(board);
    }
}
