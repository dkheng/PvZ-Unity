using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChineseZombie : Zombie
{
    public bool isCaptain = false;   //是否为队长
    public ChineseZombie prior;   //队中前一个僵尸
    public ChineseZombie next;    //队中后一个僵尸

    bool isAttacking = false;   //是否正在吃，用于符落时换动画

    //重写Start函数，不赋予随机速度增幅
    protected override void Start()
    {
        //一定概率在一段时间后暴走
        if(Random.Range(0.0f, 10.0f) < 4.0f)
        {
            Invoke("paperDisappear", Random.Range(15.0f, 30.0f));
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row)
        {
            myAnimator.SetBool("Walk", false);
            myAnimator.SetBool("Attack", true);
            //不是队长，升为队长
            if(!isCaptain)
            {
                isCaptain = true;
                prior.next = null;
                prior = null;
            }
            //队长开始吃，队员都不前进
            stopFollower();
            plant = collision.GetComponent<Plant>();
        }
        else if (collision.tag == "GameOverLine")
        {
            GameObject.Find("Game Management").GetComponent<GameManagement>().gameOver();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row)
        {
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Walk", true);
            //队长开始前进，队员都前进
            if (isCaptain) startFollower();
        }
    }

    private void paperDisappear()
    {
        //切换动画
        if(myAnimator.GetBool("Attack"))
        {
            isAttacking = true;
            myAnimator.SetBool("Mad", true);
        }
        else
        {
            myAnimator.SetBool("Walk", false);
            myAnimator.SetBool("Mad", true);
        }
    }

    private void mad()
    {
        //符消失
        transform.Find("paper").gameObject.SetActive(false);

        //切换头形态
        transform.Find("head").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "Mad");

        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/zombie_angry")
        );

        //换回动画
        if (!isAttacking)
        {
            myAnimator.SetBool("Walk", true);
        }
        myAnimator.SetBool("Mad", false);

        //行动加速
        float randAmp = Random.Range(1.5f, 2.0f);  //随机增幅
        speed *= randAmp;
        myAnimator.speed *= randAmp;

        attackPower *= 2;

        //暴走者自己成队
        isCaptain = true;
        startFollower();
        if (prior != null) prior.next = null;
        if (next != null) next.isCaptain = true;
        prior = null;
        next = null;
    }

    private void stopFollower()
    {
        for (ChineseZombie nowCZ = this; nowCZ.next != null; nowCZ = nowCZ.next)
        {
            nowCZ.next.stopWalk();
        }
    }

    private void startFollower()
    {
        for (ChineseZombie nowCZ = this; nowCZ.next != null; nowCZ = nowCZ.next)
        {
            nowCZ.next.startWalk();
        }
    }

    protected override void die()
    {
        //队长死了，后面的僵尸升为队长
        if(next != null)
        {
            next.isCaptain = true;
            next.prior = null;
        }

        base.die();
    }

    public void playSkipAudio()
    {
        audioSource.Play();
    }

    public void stopWalk()
    {
        myAnimator.SetBool("Walk", false);
    }

    public void startWalk()
    {
        myAnimator.SetBool("Walk", true);
    }
}
