using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicControl : MonoBehaviour
{
    Animator animator;

    string musicName;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animator.enabled = false;
    }

    public void changeMusicSmoothly(string name)
    {
        animator.enabled = true;
        musicName = name;
    }

    public void changeMusic()
    {
        GetComponent<AudioSource>().clip =
            Resources.Load<AudioClip>("Sounds/Background/" + musicName);
        GetComponent<AudioSource>().Play();
    }

    public void changeMusic(string name)
    {
        GetComponent<AudioSource>().clip =
            Resources.Load<AudioClip>("Sounds/Background/" + name);
        GetComponent<AudioSource>().Play();
    }

    public void disableAnimator()
    {
        animator.enabled = false;
    }
}
