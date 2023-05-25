using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject rightHand;
    public GameObject leftHand;

    Animator ani;   
    public SimpleShoot simple;
    public GameObject target;
    public float atkRange = 0;

    public Level stat;
    public Weapon curWeapon;

    void Start()
    {
        GameManager.Instance.player = this;
        stat = Level.instance;

        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
        EquipWeapon(stat.currentWeapon);

    }

    public float GetDmg()
    {
        float dmg = stat.AtkDmg;
        if (Random.Range(0, 1) <= stat.Critprob)
        {
            dmg *= stat.CritDmg / 100;
        }

        return dmg;
    }
    void Update()
    {
        if (stat.CurHp < stat.MaxHp && stat.CurHp > 0)
        {
            stat.curHp += stat.MaxHp * stat.Recov / 100 * Time.deltaTime;
        }
        ani.SetFloat("AtkSpeed", stat.AtkSpeed / 100);

        if (stat.currentWeapon == null)
        {
            ani.SetBool("IsRifle", false);
            ani.SetBool("IsKnife", false);
            ani.SetBool("IsSword", false);
            ani.SetBool("IsPistol", false);
        }
        else
        {
            switch (stat.currentWeapon.info.Type)
            {
                case WeaponInfo.TYPE.Knife:
                    ani.SetBool("IsRifle", false);
                    ani.SetBool("IsKnife", true);
                    ani.SetBool("IsSword", false);
                    ani.SetBool("IsPistol", false);
                    break;
                case WeaponInfo.TYPE.Sword:
                    ani.SetBool("IsRifle", false);
                    ani.SetBool("IsKnife", false);
                    ani.SetBool("IsSword", true);
                    ani.SetBool("IsPistol", false);
                    break;
                case WeaponInfo.TYPE.Rifle:                    
                    ani.SetBool("IsRifle", true);
                    ani.SetBool("IsKnife", false);
                    ani.SetBool("IsSword", false);
                    ani.SetBool("IsPistol", false);
                    break;
                case WeaponInfo.TYPE.Pistol:                   
                    ani.SetBool("IsRifle", false);
                    ani.SetBool("IsKnife", false);
                    ani.SetBool("IsSword", false);
                    ani.SetBool("IsPistol", true);
                    break;
            }
        }
    }

    public Weapon EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            return null;

        Weapon retWp = null;

        if (stat.currentWeapon != null)
        {
            ItemData.instance.Search(stat.currentWeapon).TryGetComponent(out retWp);
            stat.currentWeapon = null;

            if (curWeapon != null)
            {
                Destroy(curWeapon.gameObject);
            }
        }
        ani.SetTrigger("Swap");
        GameObject wp = Instantiate(weapon.gameObject);
        curWeapon = wp.GetComponent<Weapon>();
        stat.currentWeapon = weapon;

        atkRange = stat.AtkRange;      

        if (curWeapon.isLeft)
            curWeapon.transform.parent = leftHand.transform;
        else
            curWeapon.transform.parent = rightHand.transform;


        curWeapon.transform.localPosition = curWeapon.info.pos;
        curWeapon.transform.localRotation = Quaternion.Euler(curWeapon.info.rot);

        return retWp;
    }

    public void UnEquipWeapon()
    {
        stat.currentWeapon = null;

        if (curWeapon != null)
        {
            Destroy(curWeapon.gameObject);
        }
        ani.SetTrigger("Swap");
    }
    public void Fire()
    {
        simple?.Fire();
        try
        {
            GameObject b = Instantiate(Bullet, curWeapon.firePos.position, curWeapon.firePos.rotation);

            b.GetComponent<Bullet>().SetTarget(target);
            Destroy(b, 5f);
        }
        catch
        {

        }
    }
    public void FullHeal()
    {
        stat.curHp = stat.MaxHp;
    }
    public void Death()
    {
        ani.applyRootMotion = true;
        GetComponent<PlayerControl>().isStopped = true;
        GameManager.Instance.isStop = true;
        ani.SetTrigger("Death");
        Invoke("Disable", 5f);

    }

    private void OnEnable()
    {
        GetComponent<PlayerControl>().isStopped = false;
        GameManager.Instance.isStop = false;
    }
    void Disable()
    {
        Scene scene = SceneManager.GetActiveScene();
        stat.curHp = stat.MaxHp;
        SceneManager.LoadScene(scene.name);
    }
    public void OnHit(float dmg)
    {
        if (stat.CurHp > 0)
        {
            if (Random.Range(0f, 1f) > (stat.Avoid / 100))
            {
                float incDmg = dmg / (1 + stat.Def / 100);
                stat.curHp -= incDmg;
                UIManager.instance.CreateDmgText(incDmg, transform, Color.red);
                if (stat.CurHp <= 0)
                {
                    Death();
                }
            }
            else
            {
                UIManager.instance.CreateDmgText(0, transform, Color.red, true);
            }
        }
    }
}
