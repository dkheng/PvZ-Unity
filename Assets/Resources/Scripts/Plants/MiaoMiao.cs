using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiaoMiao : MultiImagePlant
{
    public GameObject parasiticSeed;   //寄生种子预制体
    public GameObject leafKnife;   //飞叶刀预制体
 
    public bool prepareParasitic = false;   //是否应发射寄生种子
    private Vector3 bulletOffset = new Vector3(0.054f, 0.218f, 0);   //子弹初始位置偏移量
    private Vector2 castEndPoint;   //射线投射时终点

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector2(5.3f, transform.position.y);
    }

    public void fireEvent()
    {
        if(!prepareParasitic)
        {
            Instantiate(leafKnife,
                        transform.position + bulletOffset,
                        Quaternion.Euler(0, 0, 0))
                .GetComponent<LeafKnife>().initialize(this, row);
            audioSource.Play();
            return;
        }
        else
        {
            RaycastHit2D[] hitResults = 
                Physics2D.LinecastAll(transform.position, castEndPoint, LayerMask.GetMask("Zombie"));
            foreach(RaycastHit2D hitResult in hitResults)
            {
                if(hitResult.transform.GetComponent<Zombie>().pos_row == row)
                {
                    //发射寄生种子
                    Instantiate(parasiticSeed,
                                transform.position + bulletOffset,
                                Quaternion.Euler(0, 0, 0))
                        .GetComponent<ParasiticSeed>()
                        .initialize(hitResult.transform.GetComponent<Zombie>(), this, row);
                    audioSource.Play();
                    prepareParasitic = false;
                    return;
                }
            }

            //探测未发现僵尸，发射飞叶刀
            Instantiate(leafKnife,
                        transform.position + bulletOffset,
                        Quaternion.Euler(0, 0, 0))
                .GetComponent<LeafKnife>().initialize(this, row);
            audioSource.Play();
            return;
        }
        
    }

}
