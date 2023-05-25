using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    Vector3 destination;    // 목적지
    Animator ani;   // 애니메이터

    NavMeshAgent nav;   // 네비게이션

    GameObject target;  // 선택된 타겟
    public bool isStopped = false;  // 정지여부
    Player player;  // 플레이어 스크립트

    public float baseSpeed; // 기본 이동속도

    void Start()
    {
        ani = GetComponent<Animator>(); // 애니메이터 가져오기
        ani.applyRootMotion = false; // 루트모션 비활성화
        nav = GetComponent<NavMeshAgent>(); // 네비게이션 가져오기
        player = GetComponent<Player>();    // 플레이어 스크립트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return; 

        nav.speed = baseSpeed * (player.stat.MoveSpeed / 100);
        
        if (ani.GetBool("IsRun") && !ani.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !ani.GetCurrentAnimatorStateInfo(0).IsName("reload"))
        {
            nav.isStopped = false;
        }
        else
        {
            nav.isStopped = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClickMove();
        }

        if (Vector3.Distance(transform.position, destination) < 0.5)
        {
            ani.SetBool("IsRun", false);
        }

        if (target != null)
        {
            destination = target.transform.position;
            nav.SetDestination(destination);

            if (GameManager.Instance.player.atkRange > Vector3.Distance(transform.position, target.transform.position))
            {
                Vector3 d = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

                Quaternion rot = Quaternion.LookRotation(d - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10 * Time.deltaTime);
                nav.isStopped = true;

                if (Quaternion.Angle(transform.rotation, rot) < 10)
                {
                    ani.SetBool("IsFire", true);
                    ani.SetBool("IsRun", false);
                }
            }
            else
            {
                nav.isStopped = false;
                ani.SetBool("IsFire", false);
            }

        }
        else
        {
            ani.SetBool("IsFire", false);
        }

    }

    void Attack()
    {
        if (target != null)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy?.OnHit(player.stat.AtkDmg);
        }
    }
    public void Stop()
    {
        isStopped = true;
        nav.isStopped = true;
        target = null;
        ani.SetBool("IsRun", false);
        ani.SetBool("IsFire", false);   
    }

    public void Move()
    {
        isStopped = false;
    }

    void ClickMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, LayerMask.GetMask("Ground", "Enemy", "NPC")) && !GameManager.Instance.isStop)
        {
            destination = hit.point;

            ani.SetBool("IsRun", true);

            if (hit.collider.CompareTag("Enemy"))
            {
                target = hit.collider.gameObject;
                nav.SetDestination(target.transform.position);

                GameManager.Instance.player.target = target;

            }
            else if (hit.collider.CompareTag("NPC"))
            {
                target = null;
                string name = hit.collider.name;
                hit.collider.GetComponent<NPC>().StartBlock(name);

                ani.SetBool("IsFire", false);
                nav.SetDestination(destination);
            }
            else
            {
                target = null;
                ani.SetBool("IsFire", false);
                nav.SetDestination(destination);
            }
        }
    }
}
