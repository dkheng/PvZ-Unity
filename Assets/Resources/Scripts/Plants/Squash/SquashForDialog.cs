using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashForDialog : Plant
{
    bool drop = true;
    Vector3 speed_drop;

    protected override void Awake()
    {
        transform.localPosition += new Vector3(0, 4, 0);
        speed_drop = Vector3.down * 4 / 0.167f;
    }

    private void Update()
    {
        if(drop == true)
        {
            transform.Translate(speed_drop * Time.deltaTime);
        }
    }

    public void playAudio()
    {
        drop = false;
        GetComponent<AudioSource>().Play();
    }
}
