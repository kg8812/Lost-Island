using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;

    public float exp = 0;
    public float maxExp = 100;
    public float maxHp = 100;
    public float atkDmg = 10;
    public float atkSpeed = 100;
    public float moveSpeed = 100;
    public float atkRange; // 공격 거리
    public float avoid = 0; //회피율
    public float hitrate = 100; //명중률
    public float cc = 0;  //치명타확률
    public float cd = 150; //치명타데미지 
    public float def = 5; //방어력
    public float reco = 1; //채력회복력
    public float curHp = 100;

    public float CurHp
    {
        get
        {
            return curHp;
        }       
    }
    public float Exp
    {
        get { return exp; }        
    }
    public float MaxExp
    {
        get { return maxExp; }
    }
    public float MaxHp
    {
        get { return maxHp; }
        
    }
    public float AtkDmg
    {
        get
        {
            return atkDmg + (currentWeapon?.info.atk ?? 0);
        }
        
    }
    public float AtkSpeed
    {
        get { return atkSpeed + (currentWeapon?.info.atkSpeed ?? 0); }
        
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        
    }
    public float AtkRange
    {
        get { return atkRange + (currentWeapon?.info.range ?? 0); }
        
    }
    public float Avoid
    {
        get { return avoid; }

    }
    public float Hitrate
    {
        get { return hitrate; }        
    }
    public float Critprob
    {
        get { return cc + (currentWeapon?.info.critProb ?? 0); }        
    }
    public float CritDmg
    {
        get { return cd + (currentWeapon?.info.critDmg ?? 0); }
        
    }
    public float Def
    {
        get { return def; }
        
    }
    public float Recov
    {
        get { return reco; }     
    }

    float addmaxHp = 10;
    float addatkDmg = 1;
    float addatkSpeed;
    float addmoveSpeed = 1;
    float addavoid = 0.5f; //회피율
    float addhitrate = 3; //명중률
    float addcc = 1;  //치명타확률
    float addcd = 2; //치명타데미지 
    float adddef = 0.5f; //방어력
    float addreco = 0.2f; //채력회복력

    public int pluseLevel = 1;

    public Weapon currentWeapon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Levelup()
    {
        if (exp >= maxExp)  //넘치는 exp 어떻게 할지 고민
        {
            pluseLevel++;
            exp = 0;
            //maxExp 늘려야함
            maxHp += addmaxHp;
            atkDmg += addatkDmg;
            atkSpeed += addatkSpeed;
            moveSpeed += addmoveSpeed;
            atkRange += atkRange;
            avoid += addavoid;
            hitrate += addhitrate;
            cc += addcc;
            cd += addcd;
            def += adddef;
            reco += addreco;

            maxExp += maxExp * 0.05f;  //10번째 마다 10% 추가하기
            Status.instance.plus += 1;
        }

    }

}
