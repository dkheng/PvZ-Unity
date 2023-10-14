using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyDave : MonoBehaviour
{
    GameObject speechBubble;   //�Ӷ���-����ĶԻ���

    string dialogToBeShowed;   //��˵�ĶԻ�
    AudioSource audioSource;   //����AudioSource���

    private void Awake()
    {
        //��ȡ���������
        speechBubble = transform.Find("SpeechBubble").gameObject;
        speechBubble.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void talk(string content)
    {
        GetComponent<Animator>().SetBool("talk", true);
        dialogToBeShowed = content;
    }

    public void smallTalk(string content)
    {
        GetComponent<Animator>().SetBool("smallTalk", true);
        dialogToBeShowed = content;
    }

    //talk������ͷ�����¼�
    public void showSpeechBubble_talk()
    {
        GetComponent<Animator>().SetBool("talk", false);   //��־λ��٣���ֻ˵һ��
        speechBubble.GetComponent<SpeechBubble>().showDialog(dialogToBeShowed);   //չʾ�Ի�
        //������Ч
        audioSource.clip =
            Resources.Load<AudioClip>("Sounds/CrazyDave/CrazyDave_Talk" + Random.Range(1, 4));
        audioSource.Play();
    }

    //smallTalk������ͷ�����¼�
    public void showSpeechBubble_smallTalk()
    {
        GetComponent<Animator>().SetBool("smallTalk", false);   //��־λ��٣���ֻ˵һ��
        speechBubble.GetComponent<SpeechBubble>().showDialog(dialogToBeShowed);   //չʾ�Ի�
        //������Ч
        audioSource.clip =
            Resources.Load<AudioClip>("Sounds/CrazyDave/CrazyDave_Short" + Random.Range(1, 4));
        audioSource.Play();
    }

    public void leave()
    {
        GetComponent<Animator>().SetBool("leave", true);
    }

    public void haveGone()
    {
        gameObject.SetActive(false);
    }
}
