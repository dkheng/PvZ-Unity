using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    public GameObject flowersunPrefab;   //向日葵太阳预制体

    Transform sunManagement;   //太阳管理器对象Tranform组件，为所有太阳父对象
    float createSunSpeed = 24f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sunManagement = GameObject.Find("Sun Management").GetComponent<Transform>();

        Invoke("createSun", 5);
    }

    private void createSun()
    {
        //播放音效
        audioSource.Play();

        //生成太阳
        Instantiate(flowersunPrefab, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);

        Invoke("createSun", createSunSpeed);
    }

    public override void cold()
    {
        base.cold();
        createSunSpeed = 48f;
    }

    public override void warm()
    {
        base.warm();
        createSunSpeed = 24f;
    }

    public override void normal()
    {
        base.normal();
        createSunSpeed = 24f;
    }

    protected override void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
        createSunSpeed = 16f;
    }

    protected override void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
        createSunSpeed = 24f;
    }
}
