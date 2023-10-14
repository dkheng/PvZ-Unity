using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafKnife : StraightBullet
{
    private MiaoMiao myCreater;

    protected override void attack(Zombie target)
    {
        //������Ч
        AudioSource.PlayClipAtPoint(
            Resources.Load<AudioClip>("Sounds/Plants/KnifeKill"),
            new Vector3(0, 0, -10)
        );
        //��ʬ������
        target.beAttacked(hurt);

        //�����ʬû����������֪ͨ����Ӧ�÷����������
        if (target.state != ZombieState.Parasiticed && myCreater != null)
            myCreater.prepareParasitic = true;
    }

    public void initialize(MiaoMiao miao, int row)
    {
        myCreater = miao;
        this.row = row;
    }

    public void initialize(MiaoMiao miao, int row, int hurt)
    {
        myCreater = miao;
        this.row = row;
        this.hurt = hurt;
    }
}
