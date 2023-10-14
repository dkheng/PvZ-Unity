using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class BucketZombie : Zombie
{
    public GameObject bucket;

    private BucketZombieState bucketState = BucketZombieState.BucketCompete;

    protected override void Awake()
    {
        base.Awake();
        audioOfBeingAttacked = "Sounds/Zombies/buckethit";
    }

    //±»¹¥»÷
    //×´Ì¬±¸×¢£º1300ÂúÑª£¬950ÌúÍ°ËğÉË1,600ÌúÍ°ËğÉË2,200ÌúÍ°µôÂä£¬100¸ì²²µôÂä
    public override void beAttacked(int hurt)
    {
        base.beAttacked(hurt);
        if (bloodVolume <= 950 && bucketState == BucketZombieState.BucketCompete)
        {
            bucketDamage1();
        }
        else if (bloodVolume <= 600 && bucketState == BucketZombieState.BucketIncomplete1)
        {
            bucketDamage2();
        }
        else if (bloodVolume <= 200 && bucketState == BucketZombieState.BucketIncomplete2)
        {
            fallBucket();
        }
        else if (bloodVolume <= 100 && bucketState == BucketZombieState.NoBucket)
        {
            fallArm();
        }
    }

    private void bucketDamage1()
    {
        bucket.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Zombies/BucketZombie/Zombie_bucket2");
        bucketState = BucketZombieState.BucketIncomplete1;
    }

    private void bucketDamage2()
    {
        bucket.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Zombies/BucketZombie/Zombie_bucket3");
        bucketState = BucketZombieState.BucketIncomplete2;
    }

    private void fallBucket()
    {
        bucket.gameObject.SetActive(false);
        audioOfBeingAttacked = "Sounds/Zombies/bodyhit";
        bucketState = BucketZombieState.NoBucket;
    }

    private void fallArm()
    {
        transform.Find("Zombie_outerarm_hand").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_lower").gameObject.SetActive(false);
        transform.Find("Zombie_outerarm_upper").GetComponent<SpriteResolver>()
            .SetCategoryAndLabel("arm", "incomplete");

        bucketState = BucketZombieState.NoArm;
    }

    protected override void hideHead()
    {
        transform.Find("Zombie_head").gameObject.SetActive(false);
        transform.Find("Zombie_jaw").gameObject.SetActive(false);
    }
}

enum BucketZombieState { BucketCompete, BucketIncomplete1, BucketIncomplete2, NoBucket, NoArm };