using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;   //�ƶ��ٶ�
    public float eatOffset;   //��ֲ��λ��ƫ�ƣ�����ֲ�����Լ���߶�Զʱ�Ͳ�����
    public int pos_row;   //λ�ڵڼ���
    public ZombieState state = ZombieState.Normal;
    private Plant parasiticPlant;   //����״̬�¼����Լ���ֲ��

    //�������
    public int bloodVolume;   //Ѫ��
    protected int bloodVolumeMax;
    private bool alive = true;

    //�������
    public int attackPower;  //������
    protected Plant plant;   //��ǰ������ֲ���Plant���

    protected Animator myAnimator;   //�������
    protected AudioSource audioSource;  //����AudioSource���
    protected string audioOfBeingAttacked = "Sounds/Zombies/bodyhit";
    private int audioIndex = 1;

    static int orderOffset = 0;

    bool sleep = true;   //�Ƿ��г�ʼ��ֹ

    protected virtual void Awake()
    {
        //��ȡ���
        myAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //��ʬ��ʼ�����ֹһ��ʱ�䣬ʹ��ʬ�ж�����ô���뻮һ
        if (sleep == true)
        {
            gameObject.SetActive(false);
            Invoke("activate", Random.Range(0.0f, 5.0f));
        }

        //�������ٶ�����
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
        //ֲ�ﱻ����
        if (plant != null)
        {
            plant.beAttacked(attackPower, "beEated");
        }
    }

    protected virtual void die()
    {
        //��ײ��ʧЧ
        gameObject.GetComponent<Collider2D>().enabled = false;
        //ȫ����ʬ����һ
        GameObject.Find("Zombie Management").GetComponent<ZombieManagement>().minusZombieNumAll();
        alive = false;
        //����ͷ
        hideHead();
        //�����л�
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Die", true);
    }

    //���ڸ�����ʬͷ���ֿ��ܲ�ͬ���ʸú�����������д
    protected virtual void hideHead()
    {

    }

    //������
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

    //�����ˣ������湥��ʱ����
    public virtual void beBurned()
    {
        beAttacked(10);
    }

    public virtual void beSquashed()
    {
        bloodVolume -= 1800;
        if(bloodVolume <= 0)
        {
            //ȫ����ʬ����һ
            GameObject.Find("Zombie Management").GetComponent<ZombieManagement>().minusZombieNumAll();
            //��ʬ��ʧ
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

    //���������У����������������������ʾ˳��
    public virtual void setPosRow(int pos)
    {
        //����������
        pos_row = pos;

        //����˳��ͼ�㼰��ʾ˳��
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

    //���Ž�ʬ���µ���Ч
    public virtual void fallDown()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/zombie_falling")
        );
    }

    //���Ž�ʬ��ҧ����Ч
    public virtual void PlayEatAudio()
    {
        audioSource.PlayOneShot(
            Resources.Load<AudioClip>("Sounds/Zombies/chomp" + Random.Range(1, 3))
        );
    }

    //��ʬ���º�ʬ����ʧ
    public void disappear()
    {
        Destroy(gameObject);
    }

}

public enum ZombieState { Normal, Cold, Parasiticed }