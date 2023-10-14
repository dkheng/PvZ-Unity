using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class NormalZombie : Zombie
{
    bool haveArm = true;   //是否有胳膊

    private void fallArm()
    {
        transform.Find("Zombie_outerarm_hand").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_lower").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_upper").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("arm", "incomplete");

        haveArm = false;
    }

    //被攻击
    public override void beAttacked(int hurt)
    {
        base.beAttacked(hurt);
        if (bloodVolume <= 100 && haveArm == true)
        {
            fallArm();
        }
    }

    protected override void hideHead()
    {
        transform.Find("Zombie_head").gameObject.SetActive(false);
        transform.Find("Zombie_jaw").gameObject.SetActive(false);
    }
}
