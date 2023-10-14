using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SunBase : MonoBehaviour
{
    public int sunNumber;   //值多少阳光

    protected bool dropState;  //阳光是否正在掉落
    bool pickState;  //阳光是否被拾取
    Vector3 finalPos = new Vector3(-4.47f, 2.61f, 0f);  //拾取阳光动画终点
    float timer = 0, disappearTime = 15.0f;   //计时器，阳光多久后消失
    SpriteRenderer mySpriteRenderer;   //用于阳光逐渐消失

    //用于增加阳光
    SunNumber sunControl;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        dropState = true;
        pickState = false;
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sunControl = GameObject.Find("Sun Text").GetComponent<SunNumber>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (dropState == true) drop();
        if (pickState == true) collect();
        if (pickState == false && timer > disappearTime) disappear();
    }

    public abstract void drop();

    public void bePickedUp()
    {
        dropState = false;
        pickState = true;

        //播放音效
        GetComponent<AudioSource>().Play();
    }

    private void collect()
    {
        if (Vector3.Distance(transform.position, finalPos) > 0.1f)  //不在终点，向终点移动
        {
            transform.Translate((finalPos - transform.position) * 4 * Time.deltaTime);
        }
        else   //在终点，阳光数增加，销毁该GameObject
        {
            sunControl.addSun(sunNumber);
            Destroy(gameObject);
        }
    }

    private void disappear()
    {
        float alpha = 1 - (timer - disappearTime) * 3;
        if (alpha > 0) mySpriteRenderer.color = new Color(255, 255, 255, alpha);
        else Destroy(gameObject);
    }
}
