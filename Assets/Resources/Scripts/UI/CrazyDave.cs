using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyDave : MonoBehaviour
{
    GameObject speechBubble;   //子对象-戴夫的对话框

    string dialogToBeShowed;   //待说的对话
    AudioSource audioSource;   //自身AudioSource组件

    private void Awake()
    {
        //获取对象与组件
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

    //talk动画开头调用事件
    public void showSpeechBubble_talk()
    {
        GetComponent<Animator>().SetBool("talk", false);   //标志位设假，即只说一遍
        speechBubble.GetComponent<SpeechBubble>().showDialog(dialogToBeShowed);   //展示对话
        //播放音效
        audioSource.clip =
            Resources.Load<AudioClip>("Sounds/CrazyDave/CrazyDave_Talk" + Random.Range(1, 4));
        audioSource.Play();
    }

    //smallTalk动画开头调用事件
    public void showSpeechBubble_smallTalk()
    {
        GetComponent<Animator>().SetBool("smallTalk", false);   //标志位设假，即只说一遍
        speechBubble.GetComponent<SpeechBubble>().showDialog(dialogToBeShowed);   //展示对话
        //播放音效
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
