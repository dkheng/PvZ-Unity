using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletAnimationSwitch : StraightBullet
{
    protected override void boom()
    {
        //×Óµ¯Õ¨¿ª
        gameObject.GetComponent<Animator>().SetBool("Boom", true);
        gameObject.GetComponent<AudioSource>().Play();
        transform.Find("Shadow").gameObject.SetActive(false);
        boomState = true;
    }
}
