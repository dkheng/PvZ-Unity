using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChineseZombie : Zombie
{
    public bool isCaptain = false;   //�Ƿ�Ϊ�ӳ�
    public ChineseZombie prior;   //����ǰһ����ʬ
    public ChineseZombie next;    //���к�һ����ʬ

    bool isAttacking = false;   //�Ƿ����ڳԣ����ڷ���ʱ������

    //��дStart����������������ٶ�����
    protected override void Start()
    {
        //һ��������һ��ʱ�����
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
            //���Ƕӳ�����Ϊ�ӳ�
            if(!isCaptain)
            {
                isCaptain = true;
                prior.next = null;
                prior = null;
            }
            //�ӳ���ʼ�ԣ���Ա����ǰ��
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
            //�ӳ���ʼǰ������Ա��ǰ��
            if (isCaptain) startFollower();
        }
    }

    private void paperDisappear()
    {
        //�л�����
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
        //����ʧ
        transform.Find("paper").gameObject.SetActive(false);

        //�л�ͷ��̬
        transform.Find("head").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "Mad");

        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/zombie_angry")
        );

        //���ض���
        if (!isAttacking)
        {
            myAnimator.SetBool("Walk", true);
        }
        myAnimator.SetBool("Mad", false);

        //�ж�����
        float randAmp = Random.Range(1.5f, 2.0f);  //�������
        speed *= randAmp;
        myAnimator.speed *= randAmp;

        attackPower *= 2;

        //�������Լ��ɶ�
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
        //�ӳ����ˣ�����Ľ�ʬ��Ϊ�ӳ�
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
