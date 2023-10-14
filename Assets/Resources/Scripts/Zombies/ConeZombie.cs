using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ConeZombie : Zombie
{
    public GameObject cone;

    private ConeZombieState coneState = ConeZombieState.ConeCompete;

    protected override void Awake()
    {
        base.Awake();
        audioOfBeingAttacked = "Sounds/Zombies/conehit";
    }

    //±»¹¥»÷
    //×´Ì¬±¸×¢£º640ÂúÑª,500Â·ÕÏËðÉË1,350Â·ÕÏËðÉË2,200Â·ÕÏµôÂä,100¸ì²²µôÂä
    public override void beAttacked(int hurt)
    {
        base.beAttacked(hurt);
        if (bloodVolume <= 500 && coneState == ConeZombieState.ConeCompete)
        {
            coneDamage1();
        }
        else if (bloodVolume <= 350 && coneState == ConeZombieState.ConeIncomplete1)
        {
            coneDamage2();
        }
        else if (bloodVolume <= 200 && coneState == ConeZombieState.ConeIncomplete2)
        {
            fallCone();
        }
        else if (bloodVolume <= 100 && coneState == ConeZombieState.NoCone)
        {
            fallArm();
        }
    }

    private void coneDamage1()
    {
        cone.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Zombies/ConeZombie/Zombie_cone2");
        coneState = ConeZombieState.ConeIncomplete1;
    }

    private void coneDamage2()
    {
        cone.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Zombies/ConeZombie/Zombie_cone3");
        coneState = ConeZombieState.ConeIncomplete2;
    }

    private void fallCone()
    {
        cone.gameObject.SetActive(false);
        audioOfBeingAttacked = "Sounds/Zombies/bodyhit";
        coneState = ConeZombieState.NoCone;
    }

    private void fallArm()
    {
        transform.Find("Zombie_outerarm_hand").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_lower").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_upper").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("arm", "incomplete");

        coneState = ConeZombieState.NoArm;
    }

    protected override void hideHead()
    {
        transform.Find("Zombie_head").gameObject.SetActive(false);
        transform.Find("Zombie_jaw").gameObject.SetActive(false);
    }
}

enum ConeZombieState { ConeCompete, ConeIncomplete1, ConeIncomplete2, NoCone, NoArm };