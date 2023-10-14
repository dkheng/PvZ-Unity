using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;   //移动速度
    public float eatOffset;   //吃植物位置偏移，就是植物在自己后边多远时就不吃了
    public int pos_row;   //位于第几行
    public ZombieState state = ZombieState.Normal;
    private Plant parasiticPlant;   //寄生状态下寄生自己的植物

    //生命相关
    public int bloodVolume;   //血量
    protected int bloodVolumeMax;
    private bool alive = true;

    //攻击相关
    public int attackPower;  //攻击力
    protected Plant plant;   //当前所攻击植物的Plant组件

    protected Animator myAnimator;   //动画组件
    protected AudioSource audioSource;  //自身AudioSource组件
    protected string audioOfBeingAttacked = "Sounds/Zombies/bodyhit";
    private int audioIndex = 1;

    static int orderOffset = 0;

    bool sleep = true;   //是否有初始静止

    protected virtual void Awake()
    {
        //获取组件
        myAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //僵尸初始随机静止一段时间，使僵尸行动不那么整齐划一
        if (sleep == true)
        {
            gameObject.SetActive(false);
            Invoke("activate", Random.Range(0.0f, 5.0f));
        }

        //添加随机速度增幅
        float increase = Random.Range(1.0f, 1.5f);
        speed *= increase;
        myAnimator.speed *= increase;

        bloodVolumeMax = bloodVolume;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (myAnimator.GetBool("Walk") == true)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant" 
            && collision.GetComponent<Plant>().row == pos_row 
            && collision.transform.position.x < transform.position.x + eatOffset
            && myAnimator.GetBool("Attack") == false)
        {
            myAnimator.SetBool("Walk", false);
            myAnimator.SetBool("Attack", true);

            plant = collision.GetComponent<Plant>();
        }
        else if (collision.tag == "GameOverLine")
        {
            GameObject.Find("Game Management").GetComponent<GameManagement>().gameOver();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row)
        {
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Walk", true);
        }
    }

    protected virtual void activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void attack()
    {
        //植物被攻击
        if (plant != null)
        {
            plant.beAttacked(attackPower, "beEated");
        }
    }

    protected virtual void die()
    {
        //碰撞体失效
        gameObject.GetComponent<Collider2D>().enabled = false;
        //全场僵尸数减一
        GameObject.Find("Zombie Management").GetComponent<ZombieManagement>().minusZombieNumAll();
        alive = false;
        //隐藏头
        hideHead();
        //动画切换
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Die", true);
    }

    //由于各个僵尸头部分可能不同，故该函数由子类重写
    protected virtual void hideHead()
    {

    }

    //被攻击
    public virtual void beAttacked(int hurt)
    {
        bloodVolume -= hurt;
        if (bloodVolume <= 0 && alive == true)
        {
            die();
        }
    }

    public virtual void playAudioOfBeingAttacked()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>(audioOfBeingAttacked + audioIndex)
        );
        if (audioIndex == 1) audioIndex = 2;
        else audioIndex = 1;
    }

    //被灼伤，被火焰攻击时调用
    public virtual void beBurned()
    {
        beAttacked(10);
    }

    public virtual void beSquashed()
    {
        bloodVolume -= 1800;
        if(bloodVolume <= 0)
        {
            //全场僵尸数减一
            GameObject.Find("Zombie Management").GetComponent<ZombieManagement>().minusZombieNumAll();
            //僵尸消失
            Destroy(gameObject);
        }
    }

    public void beParasiticed(Plant parasiticPlant)
    {
        if(state != ZombieState.Parasiticed)
        {
            SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(0.4f, 1, 0.4f, spriteRenderer.color.a);
            }
            this.parasiticPlant = parasiticPlant;
            state = ZombieState.Parasiticed;
            InvokeRepeating("suckBlood", 0, 1f);
        }
    }

    private void suckBlood()
    {
        int hurt = (int)(bloodVolumeMax * 0.01);
        beAttacked(hurt);
        if (parasiticPlant != null) parasiticPlant.recover(hurt);
    }

    public void cancelSleep()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        sleep = false;
    }

    //设置所在行，并随后依据所在行设置显示顺序
    public virtual void setPosRow(int pos)
    {
        //设置所在行
        pos_row = pos;

        //设置顺序图层及显示顺序
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer.sortingLayerName == "Default")
            {
                spriteRenderer.sortingLayerName = "Zombie-" + pos_row;
                spriteRenderer.sortingOrder += orderOffset * 20;
            }
        }
        orderOffset++;
    }

    //播放僵尸倒下的音效
    public virtual void fallDown()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/zombie_falling")
        );
    }

    //播放僵尸啃咬的音效
    public virtual void PlayEatAudio()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/chomp" + Random.Range(1, 3))
        );
    }

    //僵尸倒下后尸体消失
    public void disappear()
    {
        Destroy(gameObject);
    }

}

public enum ZombieState { Normal, Cold, Parasiticed }