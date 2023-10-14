using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiaoMiao : MultiImagePlant
{
    public GameObject parasiticSeed;   //��������Ԥ����
    public GameObject leafKnife;   //��Ҷ��Ԥ����
 
    public bool prepareParasitic = false;   //�Ƿ�Ӧ�����������
    private Vector3 bulletOffset = new Vector3(0.054f, 0.218f, 0);   //�ӵ���ʼλ��ƫ����
    private Vector2 castEndPoint;   //����Ͷ��ʱ�յ�

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
                    //�����������
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

            //̽��δ���ֽ�ʬ�������Ҷ��
            Instantiate(leafKnife,
                        transform.position + bulletOffset,
                        Quaternion.Euler(0, 0, 0))
                .GetComponent<LeafKnife>().initialize(this, row);
            audioSource.Play();
            return;
        }
        
    }

}
