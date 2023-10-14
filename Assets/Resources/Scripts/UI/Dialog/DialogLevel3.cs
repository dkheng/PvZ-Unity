using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevel3 : MonoBehaviour
{
    public SpeechBubble flowerSpeechBubble;   //向日葵对话框
    public SpeechBubble squashSpeechBubble;    //窝瓜对话框
    public SpeechBubble peaSpeechBubble;   //豌豆对话框
    public GameObject introduce1;   //僵尸介绍栏1
    public GameObject introduce2;   //僵尸介绍栏2

    GameObject flower;
    GameObject squash;
    GameObject pea;

    int count = 0;  //对话计数，当前是第几条对话

    private void Awake()
    {
        //种植参与对话的植物
        flower = GameObject.Find("Plant-2-2")
            .GetComponent<PlantGrid>().plantByGod("SunFlowerForDialog");
        pea = GameObject.Find("Plant-5-2")
            .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("flipPea", 1f);
        Invoke("flipPea", 2f);
        Invoke("peaHide", 3f);
        Invoke("showFirstTalk", 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    flowerSpeechBubble.showDialog("确实如此");
                    count++;
                    break;
                case 2:
                    flowerSpeechBubble.showDialog("但豌豆下士，你不觉得躲到指挥官背后是一种耻辱吗");
                    count++;
                    break;
                case 3:
                    peaSpeechBubble.showDialog("呃......");
                    count++;
                    break;
                case 4:
                    squash = GameObject.Find("Plant-5-2")
                        .GetComponent<PlantGrid>().plantByGod("SquashForDialog");
                    Invoke("squashTalk", 1f);
                    count = -1;
                    break;
                case 5:
                    peaSpeechBubble.showDialog("窝瓜少尉！！！");
                    count++;
                    break;
                case 6:
                    squashSpeechBubble.showDialog("作为战士，我们就应勇往直前");
                    count++;
                    break;
                case 7:
                    pea.GetComponent<Plant>().die("");
                    pea = GameObject.Find("Plant-6-2")
                        .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
                    Invoke("peaTalk2", 0.5f);
                    count = -1;
                    break;
                case 8:
                    GameObject.Find("Zombie Management").GetComponent<ZombieManagement>()
                        .createZombieByGod("Ghost", 3);
                    Invoke("peaAmazed", 1f);
                    count = -1;
                    break;
                case 9:
                    squashSpeechBubble.showDialog("大惊小怪");
                    count++;
                    break;
                case 10:
                    introduce1.SetActive(true);

                    //对话植物消失
                    flower.GetComponent<Plant>().die("");
                    squash.GetComponent<Plant>().die("");
                    pea.GetComponent<Plant>().die("");

                    count++;
                    break;
                default:
                    break;
            }
        }
    }

    private void flipPea()
    {
        pea.GetComponent<SpriteRenderer>().flipX = !(pea.GetComponent<SpriteRenderer>().flipX);
    }

    private void showFirstTalk()
    {
        peaSpeechBubble.showDialog("向日葵指挥官，这里好阴森啊");
        count++;
    }

    private void peaTalk2()
    {
        peaSpeechBubble.transform.localPosition += new Vector3(300, 0, 0);
        peaSpeechBubble.showDialog("你说得对，作为战士，我一定会挡在长官前面的");
        count = 8;
    }

    private void peaAmazed()
    {
        Instantiate(
            Resources.Load<GameObject>("Prefabs/UI/Effect/Effect_Amazed"),
            pea.transform.position + new Vector3(0.32f, 0.57f, 0),
            Quaternion.Euler(0, 0, 0),
            pea.transform
        );
        Invoke("peaHide", 1f);
    }

    private void peaHide()
    {
        pea.GetComponent<Plant>().die("");
        pea = GameObject.Find("Plant-1-2")
            .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
        if(count == -1)
        {
            peaSpeechBubble.transform.localPosition -= new Vector3(300, 0, 0);
            peaSpeechBubble.showDialog("我*！那什么玩意");
            count = 9;
        }
            
    }

    private void squashTalk()
    {
        squashSpeechBubble.showDialog("向日葵指挥官说得对");
        count = 5;
    }

    public void clickNext()
    {
        introduce1.SetActive(false);
        introduce2.SetActive(true);
    }

    public void clickStart()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/UI/graveButtonClick"),
                new Vector3(0, 0, -10)
            );
        introduce2.SetActive(false);

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }
}
