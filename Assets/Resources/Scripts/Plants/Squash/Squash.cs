using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    public BoxCollider2D collider_Idle;   //休闲时探测僵尸靠近所用碰撞体
    public Collider2D collider_attack;   //压扁僵尸所用碰撞体

    public GameObject lockedZombie;   //锁定的僵尸

    public Animator animator;   //自身Animator组件

    bool jumpingUp = false;   //是否在跳起
    bool jumpingDown = false;   //是否在落地
    public bool idle = true;
    Vector3 speed_jumpUp;   //跳起的速度
    Vector3 speed_jumpDown;   //落下的速度

    protected override void Awake()
    {
        //获取组件
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(jumpingUp == true)
        {
            transform.Translate(speed_jumpUp * Time.deltaTime);
        }
        else if(jumpingDown == true)
        {
            transform.Translate(speed_jumpDown * Time.deltaTime);
        }
    }

    public override int beAttacked(int hurt, string form)
    {
        if(idle == true)
        {
            bloodVolume -= hurt;
            if (bloodVolume <= 0)
            {
                die(form);
            }
        }
        return bloodVolume;
    }

    public void hmm()
    {
        audioSource.Play();
    }

    public void jumpUp()
    {
        Vector3 peak = lockedZombie.transform.position + new Vector3(0, 1.3f, 0);
        Vector3 destination = new Vector3(lockedZombie.transform.position.x, transform.position.y, 0);
        speed_jumpUp = (peak - transform.position) / 0.133f;   //跳起动画0.133秒
        speed_jumpDown = (destination - peak) / 0.117f;   //落下动画0.117秒
        transform.Find("Shadow").gameObject.SetActive(false);
        transform.Find("Halo").gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sortingLayerName = "PlantBullet";
        //窝瓜碰撞体失效，防止跳起来了又被吃掉
        GetComponent<BoxCollider2D>().enabled = false;
        jumpingUp = true;
    }

    public void reachPeak()
    {
        jumpingUp = false;
    }

    public void jumpDown()
    {
        jumpingDown = true;
    }

    public void reachDest()
    {
        collider_attack.enabled = false;
        jumpingDown = false;
    }

    public void squashZombie()
    {
        collider_attack.enabled = true;
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Plants/SquashFall")
        );
    }

    public void disappear()
    {
        die("");
    }

    //窝瓜寒冷时动画不减速，否则按原速度跳起会跳很远
    public override void cold()
    {
        if (state == PlantState.Normal)
        {
            state = PlantState.Cold;
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Plants/frozen"));
            GetComponent<SpriteRenderer>().color = new Color(0.33f, 0.54f, 1f);
            Invoke("coldHurt", 1f);
        }
    }

    protected override void intensify_specific()
    {
        Vector2 colliderIdleSize = collider_Idle.size;
        collider_Idle.size = new Vector2(colliderIdleSize.x * 2, colliderIdleSize.y);
    }

    protected override void cancelIntensify_specific()
    {
        Vector2 colliderIdleSize = collider_Idle.size;
        collider_Idle.size = new Vector2(colliderIdleSize.x / 2, colliderIdleSize.y);
    }
}
