using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float mainQ = QuestManager.Instance.mainQ;
            if (mainQ == 1.1f)
                QuestManager.Instance.mainQ = 1.2f;
            else if (mainQ == 2.1f)
                QuestManager.Instance.mainQ = 2.2f;
        }
    }
}
