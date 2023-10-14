using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SnowZombie : Zombie
{
    public GameObject iceShield;

    bool haveIce = false;   //是否已创造过冰块
    bool haveArm = true;   //是否有胳膊

    private void fallArm()
    {
        transform.Find("outerarm_hand").gameObject.SetActive(false);
        transform.Find("outerarm_lower").gameObject.SetActive(false);
        transform.Find("outerarm_upper").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Arm", "Incomplete");
    }

    //创造冰盾，拥有冰盾期间每秒恢复5点血量，直至回满继续行动
    public void createIce()
    {
        myAnimator.SetBool("CreateIce", false);
        
        Instantiate(iceShield,
                    transform.position + new Vector3(-0.3f, 0, 0),
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<IceShield>().init(pos_row, gameObject);
    }

    //血量恢复函数
    public void recover()
    {
        bloodVolume += 5;
        if(bloodVolume >= 400)
        {
            myAnimator.SetBool("Walk", true);
        }
    }

    public void frozePlant()
    {
        if (plant != null)
        {
            plant.cold();
        }
        myAnimator.SetBool("FrozePlant", false);
    }

    public override void attack()
    {
        base.attack();

        //一定几率吐出寒气冻结植物
        if(Random.Range(1,6) == 1 && plant != null && plant.state != PlantState.Cold)
        {
            myAnimator.SetBool("FrozePlant", true);
        }
    }

    //播放僵尸啃咬的音效
    public override void PlayEatAudio()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/attack_snowzombie")
        );
    }

    //被攻击
    public override void beAttacked(int hurt)
    {
        base.beAttacked(hurt);
        if (bloodVolume <= 200 && haveIce == false)
        {
            haveIce = true;
            myAnimator.SetBool("CreateIce", true);
            myAnimator.SetBool("Walk", false);
        }
        else if(bloodVolume <= 100 && haveArm == true)
        {
            haveArm = false;
            fallArm();
        }
    }

    //由于各个僵尸头部分可能不同，故该函数由子类重写
    protected override void hideHead()
    {
        transform.Find("head").gameObject.SetActive(false);
        transform.Find("jaw").gameObject.SetActive(false);
    }
}
