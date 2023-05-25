using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Image healthBar;
    public Image ExpBar;

    public Text hpText; //hp current/max 출력
    public Text expText; // %로 출력
    public Text levelText;

    Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Level.instance.CurHp / Level.instance.MaxHp;
        hpText.text = string.Format("{0}/{1}", Mathf.Floor(Level.instance.CurHp), Level.instance.MaxHp);
        ExpBar.fillAmount = Level.instance.Exp / Level.instance.MaxExp;
        expText.text = string.Format("{0:00}%", (Level.instance.Exp / Level.instance.MaxExp) * 100);
        levelText.text = string.Format("{0}", Level.instance.pluseLevel);
    }

}
