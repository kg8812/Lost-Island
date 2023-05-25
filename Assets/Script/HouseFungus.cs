using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class HouseFungus : MonoBehaviour
{
    public Flowchart flowchart;
    // Start is called before the first frame update
    private void Awake()
    {
        flowchart.SetFloatVariable("mainQ", QuestManager.Instance.mainQ);       
    }
    void Start()
    {
        flowchart.ExecuteBlock("분기");
    }

    // Update is called once per frame
    void Update()
    {
        flowchart.SetFloatVariable("mainQ", QuestManager.Instance.mainQ);
    }
}
