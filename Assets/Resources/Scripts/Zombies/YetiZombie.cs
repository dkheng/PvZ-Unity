using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiZombie : Zombie
{
    protected override void activate()
    {
        base.activate();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/yetiroar");
        audioSource.Play();
    }

    public override void beBurned()
    {
        beAttacked(20);
    }

    protected override void hideHead()
    {
        transform.Find("head").gameObject.SetActive(false);
        transform.Find("jaw").gameObject.SetActive(false);
    }

    //播放僵尸倒下的音效
    public override void fallDown()
    {
        audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/yetifall");
        audioSource.Play();
    }

    //播放僵尸啃咬的音效
    public override void PlayEatAudio()
    {
        audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/chomp_yeti");
        audioSource.Play();
    }
}
