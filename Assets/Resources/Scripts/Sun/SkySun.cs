using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySun : SunBase
{
    //供SkySun子类使用
    const float upperEdge = 2f;
    const float lowerEdge = -2.7f;
    protected float finalY;

    protected override void Start()
    {
        base.Start();

        finalY = Random.Range(lowerEdge, upperEdge);
    }

    public override void drop()
    {
        if(transform.position.y > finalY)
        {
            transform.Translate(0, - 0.6f * Time.deltaTime, 0);
        }
        else
        {
            dropState = false;
        }
    }
}
