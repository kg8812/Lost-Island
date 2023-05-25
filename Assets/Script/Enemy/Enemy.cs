using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public string name;
    public int level;
    public int rate;
    public float exp;
    public float maxHp;
    public float hp;
    public float atk;
    public float def;
    public float res;
    public float avd;
    public float speed;
    public float mag;
    public float reg;
    public float acc;
    public float criticalRate;
    public float ciritcalDmg;
    public float atkRagne;
    public float traceRange;
    public AnimationClip hitAni;
    public AnimationClip deathAni;
    public BoxCollider[] collides;
    Animator ani;
    NavMeshAgent agent;
    Transform player;
    Vector3 oriPos;
    public float atkTime;
    bool isAttack = false;
    public GameObject bullet = null;
    public Transform firePos;

    [Header("근거리여부")]
    [SerializeField]
    bool isMelee = true;

    [Header("선공여부")]
    [SerializeField]
    public bool isOffensive = false;

    [Header("복귀여부")]
    [SerializeField]
    bool isReturn = true;

    [Header("도망")]
    [SerializeField]
    bool isEscapable = false;
    bool isEscaped = false;
   
    [Range(0,1)]
    public float escapeProb;
   
    [SerializeField]
    float escapeHp;

    [Header("")]
    public Vector3 currentPos;
    public GameObject hpBarPrefab;
    GameObject hpBar;

    public enum EnemyState
    {
        idle,
        walk,
        attack,
        damage,
        death
    }

    public EnemyState enemyState = EnemyState.idle;

    float temp;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(hpBarPrefab, GameObject.Find("Canvas").transform);
        hpBar.GetComponent<EnemyHpBar>().SetEnemy(this);
        hpBar.transform.SetAsFirstSibling();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        oriPos = transform.position;
        ani = GetComponent<Animator>();
        isEscaped = false;
        for (int i = 0; i < collides.Length; i++)
        {
            collides[i].enabled = false;
        }
        temp = traceRange;
        if (!isOffensive)
        {           
            traceRange = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if (hp <= 0) enemyState = EnemyState.death;
        if (!isReturn) oriPos = transform.position;
        if (enemyState == EnemyState.death)
        {
            ani.SetBool("isDead", true);
            StartCoroutine(Dead());
        }
        if (enemyState == EnemyState.death) return;
        float distance = Vector3.Distance(transform.position, player.position);
        switch (enemyState)
        {
            case EnemyState.idle:
                agent.SetDestination(oriPos);
                agent.isStopped = false;
                float dist = Vector3.Distance(oriPos, transform.position);
                if (dist <= 1)
                {
                    ani.SetBool("isMove", false);
                    ani.SetBool("isIdle", true);
                    ani.SetBool("isAttack", false);
                }
                else
                {
                    ani.SetBool("isMove", true);
                    ani.SetBool("isIdle", false);
                    ani.SetBool("isAttack", false);
                }

                if (distance < traceRange)
                {

                    enemyState = EnemyState.walk;
                    agent.speed = speed;

                }
                break;
            case EnemyState.walk:
                agent.isStopped = false;
                agent.SetDestination(player.position);
                ani.SetBool("isMove", true);
                ani.SetBool("isIdle", false);
                ani.SetBool("isAttack", false);
                if (distance > traceRange)
                {

                    enemyState = EnemyState.idle;

                }
                else if (distance < atkRagne)
                {

                    enemyState = EnemyState.attack;

                }
                break;
            case EnemyState.attack:
                ani.SetBool("isMove", false);
                ani.SetBool("isIdle", false);
                ani.SetBool("isAttack", true);
                transform.LookAt(player);
                agent.isStopped = true;
                agent.SetDestination(transform.position);
                agent.velocity = Vector3.zero;
                if (!isAttack)
                {
                    if (isMelee)
                        StartCoroutine(Attack(atkTime));

                }
                break;
            case EnemyState.damage:

                break;

        }

    }

    public void OnHit(float dmg)
    {

        if (enemyState == EnemyState.damage || enemyState == EnemyState.death) return;

        if (!isOffensive)
        {          
            traceRange = temp;           
        }

        if (Random.Range(0f, 1f) > player.GetComponent<Player>().stat.Hitrate - avd)
        {
            UIManager.instance.CreateDmgText(0, transform, Color.yellow, true);
            return;
        }

        enemyState = EnemyState.damage;
        ani.SetTrigger("Hit");

        float incDmg = dmg / (1 + def / 100);

        if (hp > 0)
        {
            hp -= incDmg;
            UIManager.instance.CreateDmgText(incDmg, transform, Color.yellow, false);
            if (Random.Range(0f, 1f) < escapeProb && isEscapable && (hp / maxHp) < (escapeHp / 100))
            {
                StartCoroutine(Escape());
            }
        }
        else
        {
            ani.SetBool("isDead", true);

        }
        Invoke("SetIdle", hitAni.length);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

    }
    IEnumerator Escape()
    {
        if (!isEscaped)
        {
            traceRange = 0;
            isEscaped = true;
            yield return new WaitForSeconds(15f);
            traceRange = temp;
            isEscaped = false;
        }
    }
    public void SetIdle()
    {
        ani.SetBool("isMove", false);
        ani.SetBool("isAttack", false);
        ani.SetBool("isIdle", true);
        enemyState = EnemyState.idle;
    }
    IEnumerator Dead()
    {
        for (int i = 0; i < collides.Length; i++)
        {
            collides[i].enabled = false;
        }
        agent.enabled = false;
        yield return new WaitForSeconds(deathAni.length);
        Destroy(gameObject);
        Level.instance.exp += exp;
        //if(QuestManager.Instance.mainQ == 1.1f && name == "경비용 로봇1")
        //{
        //    player.GetComponent<Player>().Death();
        //}
        if (QuestManager.Instance.subTxt == "Sub1" && name == "실험체 1")
        {
            QuestManager.Instance.sub1++;
            if(QuestManager.Instance.sub1 == 5)
            {
               
                QuestManager.Instance.Reward(100, 5);
                QuestManager.Instance.sub1 = 6;
                QuestManager.Instance.subTxt = "";
            }
        }
    }
    IEnumerator Attack(float time)
    {
        isAttack = true;
        for (int i = 0; i < collides.Length; i++)
        {
            collides[i].enabled = true;
        }
        yield return new WaitForSeconds(time);
        isAttack = false;
        enemyState = EnemyState.walk;
        for (int i = 0; i < collides.Length; i++)
        {
            collides[i].enabled = false;
        }

    }
    public void RangeAttack()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        bullet.GetComponent<EnemyBullet>().atk = atk;
        enemyState = EnemyState.walk;


    }

    private void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentPos = collision.contacts[0].point;
        }
    }

    private void OnDestroy()
    {
        Destroy(hpBar);
    }
}
