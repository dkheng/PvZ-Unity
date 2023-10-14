using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //子对象DialogText的Text组件，用于更新字体
    public AudioSource backgroundAudio;   //背景音乐的播放组件

    public void gameOver()
    {
        //显示界面
        Time.timeScale = 0;
        dialogText.text = "僵尸吃掉了你的脑子";
        dialogText.color = new Color(0.06f, 0.79f, 0.11f);
        gameObject.SetActive(true);

        //播放音效
        backgroundAudio.Stop();
        GetComponent<AudioSource>().clip =
            Resources.Load<AudioClip>("Sounds/UI/loseMusic");
        GetComponent<AudioSource>().Play();
    }

    public void win()
    {
        Invoke("win_real", 5f);
    }

    private void win_real()
    {
        //显示界面
        Time.timeScale = 0;
        dialogText.text = "你已成功击退了僵尸";
        dialogText.color = new Color(0.89f, 0.76f, 0.37f);
        gameObject.SetActive(true);

        //播放音效
        backgroundAudio.Stop();
        GetComponent<AudioSource>().clip =
            Resources.Load<AudioClip>("Sounds/UI/winMusic");
        GetComponent<AudioSource>().Play();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
