using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caption : MonoBehaviour
{
    Queue<CaptionNode> captionQueue = new Queue<CaptionNode>();   //��չʾ��Ļ����
    SpriteRenderer spriteRenderer;   //�����SpriteRenderer���
    AudioSource audioSource;   //����AudioSource���

    //��Ļ��С����
    bool haveShrinked = false;  //��Ļ�Ƿ��Ѿ���С���
    Vector3 shrinkVelocity = new Vector3(1000, 1000, 0);  //��Ļ��С�ٶ�
    Vector3 captionMaxScale = new Vector3(200, 200, 0);   //��Ļ����ģ
    int x_MinScale = 80;   //��Ļ��С��ģ��x����

    //��ĸ��ֹչʾ
    bool isShowing = false;   //�Ƿ�����չʾ��Ļ
    float showTimer = 0;    //��Ļչʾʱ���ʱ��

    CaptionNode nowNode;  //��ǰ����չʾ����Ļ

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��ֹ��Ԥ����ʼʱ֡�ʹ���Ӱ��չʾЧ��
        gameObject.SetActive(false);
        Invoke("activate", 2f);

        //��ȡ���
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

    //������ĸ״̬
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

    //����չʾ����Ļ
    private void changeCaption()
    {
        nowNode = captionQueue.Dequeue();

        //����ͼƬ
        spriteRenderer.sprite =
                    Resources.Load<Sprite>("Sprites/UI/Caption/" + nowNode.caption);
        transform.localScale = captionMaxScale;
        spriteRenderer.enabled = true;
        isShowing = true;
        haveShrinked = false;
        showTimer = 0;

        //������Ч
        audioSource.clip = 
            Resources.Load<AudioClip>("Sounds/UI/Caption/" + nowNode.caption);
        audioSource.Play();
    }

    //׼��չʾ��Ϸ��ʼ��Ļ
    public void showGameStart()
    {
        captionQueue.Enqueue(new CaptionNode("StartReady", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartSet", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartPlant", 0.75f));
    }

    //׼��չʾһ�󲨽�ʬ��Ļ
    public void showWave()
    {
        captionQueue.Enqueue(new CaptionNode("HugeWave", 3f));
    }

    //׼��չʾ���һ����Ļ
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