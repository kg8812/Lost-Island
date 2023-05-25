using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PasueUI : MonoBehaviour
{
    public Text main;
    public Text sub;
    // Start is called before the first frame update
    private void OnEnable()
    {
        main.text = QuestManager.Instance.mainTxt;
        sub.text = QuestManager.Instance.subTxt;
    }


}
