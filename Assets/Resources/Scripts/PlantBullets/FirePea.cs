using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePea : StraightBulletAnimationSwitch
{

    protected override void attack(Zombie target)
    {
        //½©Κ¬Κάµ½Ή¥»χ
        target.beAttacked(hurt);
        target.beBurned();
    }
}
