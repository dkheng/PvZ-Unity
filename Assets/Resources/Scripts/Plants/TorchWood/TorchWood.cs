using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchWood : Plant
{
    public GameObject firePea;
    public int firePeaHurt = 30;
    public Collider2D burnRegionCollider;

    int zombieNum = 0; //在火炬灼伤范围内的僵尸数量
    List<Collider2D> zombies = new List<Collider2D>();  //在灼伤范围内的僵尸列表
    ContactFilter2D contactFilter = new ContactFilter2D();  //碰撞体探测过滤器，用于探测僵尸

    protected override void Start()
    {
        base.Start();

        warm();

        contactFilter.NoFilter();
        contactFilter.SetLayerMask(LayerMask.GetMask("Zombie"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pea")
        {
            //生成火豌豆
            Instantiate(firePea,
                        collision.transform.position,
                        Quaternion.Euler(0, 0, 0))
                .GetComponent<StraightBullet>().initialize(row, firePeaHurt);
            //销毁豌豆
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Zombie" && collision.GetComponent<Zombie>().pos_row == row)
        {
            zombieNum++;
            if(zombieNum == 1)
            {
                InvokeRepeating("burnZombie", 0.0f, 1.0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && collision.GetComponent<Zombie>().pos_row == row)
        {
            zombieNum--;
            if (zombieNum <= 0)
            {
                CancelInvoke();
            }
        }
    }

    private void burnZombie()
    {
        if(burnRegionCollider.OverlapCollider(contactFilter, zombies) != 0)
        {
            foreach(Collider2D collider in zombies)
            {
                if ( collider.GetComponent<Zombie>().pos_row == row)
                    collider.GetComponent<Zombie>().beBurned();
            }
        }
        else
        {
            zombieNum = 0;
            CancelInvoke();
        }
    }

    protected override void beforeDie()
    {
        transform.Find("WarmPlantRegion").GetComponent<WarmPlantRegion>().stopWarm();
    }

    protected override void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
        firePeaHurt = (int)(firePeaHurt * 1.5);
    }

    protected override void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
        firePeaHurt = (int)(firePeaHurt / 1.5);
    }
}
