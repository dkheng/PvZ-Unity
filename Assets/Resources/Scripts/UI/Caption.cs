using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caption : MonoBehaviour
{
    Queue<CaptionNode> captionQueue = new Queue<CaptionNode>();   //待展示字幕队列
    SpriteRenderer spriteRenderer;   //自身的SpriteRenderer组件
    AudioSource audioSource;   //自身AudioSource组件

    //字幕缩小动画
    bool haveShrinked = false;  //字幕是否已经缩小完成
    Vector3 shrinkVelocity = new Vector3(1000, 1000, 0);  //字幕缩小速度
    Vector3 captionMaxScale = new Vector3(200, 200, 0);   //字幕最大规模
    int x_MinScale = 80;   //字幕最小规模的x分量

    //字母静止展示
    bool isShowing = false;   //是否正在展示字幕
    float showTimer = 0;    //字幕展示时间计时器

    CaptionNode nowNode;  //当前正在展示的字幕

    // Start is called before the first frame update
    void Start()
    {
        //初始静止，预防开始时帧率过低影响展示效果
        gameObject.SetActive(false);
        Invoke("activate", 2f);

        //获取组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        audioSource = GetComponent<AudioSource>();

        showGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing == true)
        {
            updateCaption();
        }
        if (isShowing == false && captionQueue.Count > 0)
        {
            changeCaption();
        }
    }

    private void activate()
    {
        gameObject.SetActive(true);
    }

    //更新字母状态
    private void updateCaption()
    {
        if (haveShrinked == false)
        {
            transform.localScale -= shrinkVelocity * Time.deltaTime;
            if (transform.localScale.x <= x_MinScale)
            {
                haveShrinked = true;
            }
        }
        else
        {
            showTimer += Time.deltaTime;
            if (showTimer > nowNode.showTime)
            {
                isShowing = false;
                spriteRenderer.enabled = false;
            }
        }
    }

    //更换展示的字幕
    private void changeCaption()
    {
        nowNode = captionQueue.Dequeue();

        //更新图片
        spriteRenderer.sprite =
                    Resources.Load<Sprite>("Sprites/UI/Caption/" + nowNode.caption);
        transform.localScale = captionMaxScale;
        spriteRenderer.enabled = true;
        isShowing = true;
        haveShrinked = false;
        showTimer = 0;

        //播放音效
        audioSource.clip = 
            Resources.Load<AudioClip>("Sounds/UI/Caption/" + nowNode.caption);
        audioSource.Play();
    }

    //准备展示游戏开始字幕
    public void showGameStart()
    {
        captionQueue.Enqueue(new CaptionNode("StartReady", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartSet", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartPlant", 0.75f));
    }

    //准备展示一大波僵尸字幕
    public void showWave()
    {
        captionQueue.Enqueue(new CaptionNode("HugeWave", 3f));
    }

    //准备展示最后一波字幕
    public void showFinalWave()
    {
        captionQueue.Enqueue(new CaptionNode("HugeWave", 3f));
        captionQueue.Enqueue(new CaptionNode("FinalWave", 3f));
    }
}

public class CaptionNode
{
    public string caption;
    public float showTime;

    public CaptionNode(string caption, float showTime)
    {
        this.caption = caption;
        this.showTime = showTime;
    }
}