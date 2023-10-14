using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text dialogText;   //�Ӷ���DialogText��Text��������ڸ�������
    public AudioSource backgroundAudio;   //�������ֵĲ������

    public void gameOver()
    {
        //��ʾ����
        Time.timeScale = 0;
        dialogText.text = "��ʬ�Ե����������";
        dialogText.color = new Color(0.06f, 0.79f, 0.11f);
        gameObject.SetActive(true);

        //������Ч
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
        //��ʾ����
        Time.timeScale = 0;
        dialogText.text = "���ѳɹ������˽�ʬ";
        dialogText.color = new Color(0.89f, 0.76f, 0.37f);
        gameObject.SetActive(true);

        //������Ч
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
