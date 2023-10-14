using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class IceBlockZombie : Zombie
{
    IceBlockZombieState iceState = IceBlockZombieState.IceCompete;

    public GameObject outerarm_upper;
    public GameObject outerarm_lower;
    public GameObject outerarm_hand;
    public GameObject hat;
    public GameObject jaw;

    //±»¹¥»÷
    //×´Ì¬±¸×¢£º1300ÂúÑª£¬950±ù¿éËðÉË1,600±ù¿éËðÉË2,200±ù¿éµôÂä£¬100¸ì²²µôÂä
    public override void beAttacked(int hurt)
    {
        base.beAttacked(hurt);
        if (bloodVolume <= 950 && iceState == IceBlockZombieState.IceCompete)
        {
            iceDamage1();
        }
        else if (bloodVolume <= 600 && iceState == IceBlockZombieState.IceIncomplete1)
        {
            iceDamage2();
        }
        else if (bloodVolume <= 200 && iceState == IceBlockZombieState.IceIncomplete2)
        {
            fallIce();
        }
        else if (bloodVolume <= 100 && iceState == IceBlockZombieState.NoIce)
        {
            fallArm();
        }
    }

    //ÓÐ±ù¿éÊ±£¬»áÊÜµ½ºÜ´óµÄ»ðÉËº¦
    public override void beBurned()
    {
        if (iceState == IceBlockZombieState.IceCompete ||
            iceState == IceBlockZombieState.IceIncomplete1 ||
            iceState == IceBlockZombieState.IceIncomplete2)
        {
            if (bloodVolume >= 400) beAttacked(200);
            else beAttacked(bloodVolume - 200);
        }
        else
        {
            beAttacked(10);
        }
    }

    private void iceDamage1()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "IceIncomplete1");

        iceState = IceBlockZombieState.IceIncomplete1;
    }

    private void iceDamage2()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "IceIncomplete2");

        iceState = IceBlockZombieState.IceIncomplete2;
    }

    private void fallIce()
    {
        hat.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Head", "NoIce");
        outerarm_upper.SetActive(true);
        outerarm_lower.SetActive(true);
        outerarm_hand.SetActive(true);
        jaw.SetActive(true);

        iceState = IceBlockZombieState.NoIce;
    }

    private void fallArm()
    {
        outerarm_hand.SetActive(false);
        outerarm_lower.SetActive(false);
        outerarm_upper.GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("Arm", "Incomplete");

        iceState = IceBlockZombieState.NoArm;
    }

    protected override void hideHead()
    {
        hat.SetActive(false);
        jaw.SetActive(false);
    }
}

enum IceBlockZombieState { IceCompete, IceIncomplete1, IceIncomplete2, NoIce, NoArm };
