using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : Zombie
{
    private GameObject myZombie;

    protected override void Awake()
    {
        //获取组件
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    protected override void die()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/Zombies/IceCrack"),
                new Vector3(0, 0, -10)
            );
        if (myZombie != null)
        {
            Animator animator = myZombie.GetComponent<Animator>();
            if(animator.GetBool("Attack") == false)
            {
                animator.SetBool("Walk", true);
            }
        }
        Destroy(gameObject);
    }

    public override void beBurned()
    {
        beAttacked(100);
    }

    public override void beSquashed()
    {
        bloodVolume -= 1800;
        if (bloodVolume <= 0)
        {
            //僵尸消失
            Destroy(gameObject);
        }
    }

    public void init(int pos_row, GameObject zombie)
    {
        this.pos_row = pos_row;
        GetComponent<SpriteRenderer>().sortingLayerName = "Zombie-" + pos_row;

        this.myZombie = zombie;
    }
}
