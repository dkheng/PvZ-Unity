using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterSingle : Plant
{
    public GameObject pea;  //子弹预制体

    public void fireEvent()
    {
        //生成豌豆
        Instantiate(pea,
                    transform.position + new Vector3(0.4f, 0.14f, 0),
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<StraightBullet>().initialize(row);

        //播放音效
        audioSource.Play();
    }

}
