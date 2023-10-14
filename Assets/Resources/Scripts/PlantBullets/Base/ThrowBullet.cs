using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBullet : MonoBehaviour
{
    public float speed = 4;   //子弹速度
    public float rotateSpeed = 4;  //空中旋转速度
    public int hurt;
    public Sprite boomSprite;
    protected Plant myPlant;

    private int row;
    private bool boom = false;

    private bool moving = false;
    private Vector2 initialPos;
    private float a;
    private float b;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float delta_x = speed * Time.deltaTime;
            float x = transform.position.x - initialPos.x + delta_x;
            float y = a * x * x + b * x;
            transform.position = new Vector2(x, y) + initialPos;
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }

        if (transform.position.y < initialPos.y - 0.218f)
        {
            blast();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && boom == false && row == collision.GetComponent<Zombie>().pos_row)
        {
            blast();
            attack(collision.GetComponent<Zombie>());
        }
        else if (collision.tag == "BulletDisappearLine")
        {
            Destroy(gameObject);
        }
    }

    private void blast()
    {
        boom = true;
        moving = false;

        //切换炸开图片
        gameObject.GetComponent<SpriteRenderer>().sprite = boomSprite;
        Invoke("disappear", 0.1f);
    }

    protected virtual void attack(Zombie zombie)
    {
        zombie.playAudioOfBeingAttacked();
        zombie.beAttacked(hurt);
    }

    protected void disappear()
    {
        Destroy(gameObject);
    }


    public void initialize(Zombie targetZombie, Plant myPlant, int row)
    {
        this.row = row;
        this.myPlant = myPlant;

        //计算抛物线参数
        initialPos = transform.position;
        float distance = targetZombie.transform.position.x - initialPos.x;
        float y = distance / 3;
        float x = distance / 2;
        a = -y / (x * x);
        b = 2 * (-a) * x;
        moving = true;
    }

    public void initialize(Zombie targetZombie, Plant myPlant, int row, int hurt)
    {
        this.row = row;
        this.myPlant = myPlant;
        this.hurt = hurt;

        //计算抛物线参数
        initialPos = transform.position;
        float distance = targetZombie.transform.position.x - initialPos.x;
        float y = distance / 3;
        float x = distance / 2;
        a = -y / (x * x);
        b = 2 * (-a) * x;
        moving = true;
    }
}
