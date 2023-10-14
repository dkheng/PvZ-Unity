using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected PlantGrid myGrid;   //该植物所在Grid
    public int row;  //该植物在第几行

    public int bloodVolume;
    private int bloodVolumeMax;

    public PlantState state = PlantState.Normal;
    protected int warmSource = 0;  //周围有几个温暖源

    protected bool intensified = false;   //是否处于强化状态

    protected AudioSource audioSource;   //自身AudioSource组件

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        bloodVolumeMax = bloodVolume;
        if(SnowKingSong.play)
        {
            intensify();
        }
    }

    public virtual int beAttacked(int hurt, string form)
    {
        bloodVolume -= hurt;
        if (bloodVolume <= 0)
        {
            die(form);
        }
        return bloodVolume;
    }

    public virtual void cold()
    {
        if (state == PlantState.Normal && !intensified)
        {
            state = PlantState.Cold;
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Plants/frozen"));
            GetComponent<SpriteRenderer>().color = new Color(0.33f, 0.54f, 1f);
            GetComponent<Animator>().speed = 0.5f;
            Invoke("coldHurt", 1f);
        }
    }

    protected virtual void coldHurt()
    {
        if(state == PlantState.Cold)
        {
            beAttacked(8, "coldHurt");
            Invoke("coldHurt", 1f);
        }
    }

    public virtual void warm()
    {
        warmSource++;
        if(warmSource == 1 && !intensified)
        {
            state = PlantState.Warm;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Animator>().speed = 1f;
        }
    }

    public virtual void stopWarm()
    {
        warmSource--;
        if (warmSource <= 0 && !intensified)
        {
            normal();
        }
    }

    public virtual void normal()
    {
        state = PlantState.Normal;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Animator>().speed = 1f;
    }

    public void recover(int value)
    {
        bloodVolume += value;
        if(bloodVolume > bloodVolumeMax) bloodVolume = bloodVolumeMax;
    }

    //强化函数，执行公共操作并调用特定操作函数
    public void intensify()
    {
        if(!intensified)
        {
            intensified = true;
            normal();
            transform.Find("Halo").gameObject.SetActive(true);
            intensify_specific();
        }
    }

    //强化特定操作
    protected virtual void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
    }

    //取消强化函数，执行公共操作并调用特定操作函数
    public void cancelIntensify()
    {
        if(intensified)
        {
            intensified = false;
            transform.Find("Halo").gameObject.SetActive(false);
            if (warmSource > 0) state = PlantState.Warm;
            else state = PlantState.Normal;
            cancelIntensify_specific();
        }
    }

    //取消强化特定操作
    protected virtual void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
    }

    public virtual void attack(bool attack)
    {

    }

    public virtual void highlight()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
    }

    public virtual void cancelHighlight()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
        GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        row = grid.row;
        myGrid = grid;
    }

    public void die(string reason)
    {
        beforeDie();
        myGrid.plantDie(reason);
        Destroy(gameObject);
    }

    //死前需要处理的事，由具体植物实现
    protected virtual void beforeDie()
    {

    }
}

public enum PlantState { Normal, Warm, Cold }