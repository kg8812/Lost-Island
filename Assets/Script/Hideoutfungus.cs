using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Hideoutfungus : MonoBehaviour
{
    public Flowchart flowchart;
    private void Awake()
    {

        flowchart.SetFloatVariable("mainQ", QuestManager.Instance.mainQ);
    }
    // Start is called before the first frame update
    void Start()
    {
        flowchart.ExecuteBlock("분기");
    }

    public void Sub1()
    {
        if(QuestManager.Instance.sub1 == -1)
        QuestManager.Instance.CreateBoard("Sub1", "실험체와 5번 전투를 하자" + "\n\n보상: 경험치 100,핸드건", true);
    }
    public void Main2Clear()
    {
        QuestManager.Instance.mainQ = 3;
    }
}
