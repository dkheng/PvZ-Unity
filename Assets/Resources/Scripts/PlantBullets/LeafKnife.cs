using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafKnife : StraightBullet
{
    private MiaoMiao myCreater;

    protected override void attack(Zombie target)
    {
        //播放音效
        AudioSource.PlayClipAtPoint(
            Resources.Load<AudioClip>("Sounds/Plants/KnifeKill"),
            new Vector3(0, 0, -10)
        );
        //僵尸被攻击
        target.beAttacked(hurt);

        //如果僵尸没被寄生，就通知喵喵应该发射寄生种子
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
