using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SnowZombie : Zombie
{
    public GameObject iceShield;

    bool haveIce = false;   //�Ƿ��Ѵ��������
    bool haveArm = true;   //�Ƿ��и첲

    private void fallArm()
    {
        transform.Find("outerarm_hand").gameObject.SetActive(false);
        transform.Find("outerarm_lower").gameObject.SetActive(false);
        transform.Find("outerarm_upper").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Arm", "Incomplete");
    }

    //������ܣ�ӵ�б����ڼ�ÿ��ָ�5��Ѫ����ֱ�����������ж�
    public void createIce()
    {
        myAnimator.SetBool("CreateIce", false);
        
        Instantiate(iceShield,
                    transform.position + new Vector3(-0.3f, 0, 0),
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<IceShield>().init(pos_row, gameObject);
    }

    //Ѫ���ָ�����
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

        //һ�������³���������ֲ��
        if(Random.Range(1,6) == 1 && plant != null && plant.state != PlantState.Cold)
        {
            myAnimator.SetBool("FrozePlant", true);
        }
    }

    //���Ž�ʬ��ҧ����Ч
    public override void PlayEatAudio()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/attack_snowzombie")
        );
    }

    //������
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

    //���ڸ�����ʬͷ���ֿ��ܲ�ͬ���ʸú�����������д
    protected override void hideHead()
    {
        transform.Find("head").gameObject.SetActive(false);
        transform.Find("jaw").gameObject.SetActive(false);
    }
}
