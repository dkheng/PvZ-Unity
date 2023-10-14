using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    public float speed = 4;   //子弹速度
    public int hurt;  //子弹伤害
    public Sprite boomSprite;

    protected bool boomState = false;  //子弹是否已爆炸
    protected int row;

    // Update is called once per frame
    void Update()
    {
        if (boomState == false)  //没有炸开，则向前飞
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && boomState == false && row == collision.GetComponent<Zombie>().pos_row)
        {
            boom();
            attack(collision.GetComponent<Zombie>());
        }
        else if (collision.tag == "BulletDisappearLine")
        {
            Destroy(gameObject);
        }
    }

    protected virtual void boom()
    {
        boomState = true;

        //切换炸开图片
        gameObject.GetComponent<SpriteRenderer>().sprite = boomSprite;
        Invoke("disappear", 0.1f);
    }

    protected virtual void attack(Zombie target)
    {
        //播放音效
        target.playAudioOfBeingAttacked();
        //僵尸被攻击
        target.beAttacked(hurt);
    }

    protected void disappear()
    {
        Destroy(gameObject);
    }

    public void initialize(int row)
    {
        this.row = row;
    }

    public void initialize(int row, int hurt)
    {
        this.row = row;
        this.hurt = hurt;
    }
}
