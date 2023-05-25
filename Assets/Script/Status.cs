using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public static Status instance;

    public Text maxHp;
    public Text atkDmg;
    public Text atkSpeed;
    public Text moveSpeed;
    public Text atkRange;
    public Text avoid;
    public Text hitrate;
    public Text cc;
    public Text cd;
    public Text def;
    public Text reco;

    public Button STR;
    public Button DEX;
    public Button INT;

    public Text plusText;
    public float plus; // 레벨업시 주어지는 능력치부여 횟수

   
    
   
    void Start()
    {      
        STR.onClick.AddListener(STRClick);
        DEX.onClick.AddListener(DEXClick);
        INT.onClick.AddListener(INTClick);
    }

    private void OnEnable()
    {
        GameManager.Instance.isStop = true;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.Instance.isStop = false;
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        maxHp.text = string.Format("최대채력 : {0}", Level.instance.MaxHp);
        atkDmg.text = string.Format("공격력 : {0}", Level.instance.AtkDmg);
        atkSpeed.text = string.Format("공격속도 : {0}%", Level.instance.AtkSpeed);
        moveSpeed.text = string.Format("이동속도 : {0}%", Level.instance.MoveSpeed);
        atkRange.text = string.Format("공격범위 : {0}", Level.instance.AtkRange);
        avoid.text = string.Format("회피율 : {0}%", Level.instance.Avoid);
        hitrate.text = string.Format("명중률 : {0}%", Level.instance.Hitrate);
        cc.text = string.Format("크리티컬 확률 : {0}%", Level.instance.Critprob);
        cd.text = string.Format("크리티컬 데미지: {0}%", Level.instance.CritDmg);       
        def.text = string.Format("방어력 : {0}", Level.instance.Def);
        reco.text = string.Format("회복력 : {0}/s", Level.instance.Recov);
        plusText.text = string.Format("남은 스탯 부여횟수 {0}", plus);

        
    }

    



    void STRClick()
    {
        if (plus > 0)
        {
            Level.instance.maxHp = Level.instance.maxHp + 10;
            Level.instance.atkDmg = Level.instance.atkDmg + 1;
            Level.instance.def = Level.instance.def + 0.5f;
            plus--;
        }
    }

    void DEXClick()
    {
        if (plus > 0)
        {           
            Level.instance.avoid = Level.instance.avoid + 0.5f;
            Level.instance.hitrate = Level.instance.hitrate + 3;
            Level.instance.atkSpeed = Level.instance.atkSpeed + 3;
            Level.instance.cd = Level.instance.cd + 2;
            Level.instance.cc = Level.instance.cc + 1;

            plus--;
        }
    }

    void INTClick()
    {
        if (plus > 0)
        {
            Level.instance.reco = Level.instance.reco + 0.2f;
            Level.instance.moveSpeed = Level.instance.moveSpeed + 3;
            plus--;
        }
    }


   

}
