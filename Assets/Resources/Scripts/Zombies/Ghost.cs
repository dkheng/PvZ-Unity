using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Zombie
{

    protected override void Start()
    {
        base.Start();

        //出现在后半草坪中随机位置
        transform.localPosition = new Vector3( Random.Range(1.0f, 4.0f), transform.localPosition.y, 0);

        bloodVolume = Random.Range(60, 200);
    }

    //重写为空
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOverLine")
        {
            die();
        }
    }

    //重写为空
    protected override void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected override void die()
    {
        //碰撞体失效
        gameObject.GetComponent<Collider2D>().enabled = false;
        //动画切换
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Die", true);
    }

    //被攻击
    public override void beAttacked(int hurt)
    {
        bloodVolume -= hurt;
        if (bloodVolume <= 0)
        {
            die();
        }
    }

    public override void beSquashed()
    {
        die();
    }
}
