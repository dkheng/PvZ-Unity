using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevel4 : MonoBehaviour
{
    public SpeechBubble peaSpeechBubble;   //豌豆对话框
    public SpeechBubble woodAndFlowerSpeechBubble;   //火炬和向日葵对话框
    public SpeechBubble kingSpeechBubble;   //雪王对话框
    public GameObject zombieIntroduce1;   //僵尸介绍栏1
    public GameObject zombieIntroduce2;   //僵尸介绍栏2
    public GameObject plantIntroduce;     //植物介绍栏

    GameObject flower;
    GameObject pea;
    GameObject wood;
    GameObject snowKing;

    int count = 0;  //对话计数，当前是第几条对话
    private void Awake()
    {
        //种植参与对话的植物
        flower = GameObject.Find("Plant-0-3")
            .GetComponent<PlantGrid>().plantByGod("SunFlowerForDialog");
        pea = GameObject.Find("Plant-1-3")
            .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
    }

    // Start is called before the first frame update
    void Start()
    {
        woodAndFlowerSpeechBubble.showDialog("阿......阿嚏！");
    }

    // Update is called once per frame
    void Update()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 0:
                    peaSpeechBubble.showDialog("向日葵指挥官，你还好吗......阿嚏！");
                    count++;
                    break;
                case 1:
                    flower.GetComponent<Plant>().cold();
                    Invoke("flowerLastWords", 2f);
                    count = -1;
                    break;
                case 2:
                    flower.GetComponent<Plant>().die("");
                    Invoke("peaShock", 0.5f);
                    count = -1;
                    break;
                case 3:
                    woodAndFlowerSpeechBubble.gameObject.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Sprites/UI/SpeechBubble/SpeechBubble");
                    woodAndFlowerSpeechBubble.transform.localPosition += new Vector3(0, -71, 0);
                    woodAndFlowerSpeechBubble.showDialog("不用担心");
                    count++;
                    break;
                case 4:
                    wood = GameObject.Find("Plant-0-3")
                        .GetComponent<PlantGrid>().plantByGod("TorchWood");
                    woodAndFlowerSpeechBubble.gameObject.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Sprites/UI/SpeechBubble/SpeechBubble2");
                    woodAndFlowerSpeechBubble.transform.localPosition += new Vector3(0, 71, 0);
                    woodAndFlowerSpeechBubble.showDialog("我已经送指挥官去山洞里休息了");
                    count++;
                    break;
                case 5:
                    peaSpeechBubble.showDialog("火炬少尉！见到你太好了！");
                    count++;
                    break;
                case 6:
                    peaSpeechBubble.showDialog("（烤手手）");
                    count++;
                    break;
                case 7:
                    GetComponent<AudioSource>().Play();
                    Invoke("woodPrepare", 1.5f);
                    count = -1;
                    break;
                case 8:
                    AudioSource.PlayClipAtPoint(
                        Resources.Load<AudioClip>("Sounds/Plants/MXBC"),
                        new Vector3(0, 0, -10)
                    );
                    Invoke("snowKingAppear", 1.5f);
                    count = -1;
                    break;
                case 9:
                    peaSpeechBubble.showDialog("吓！\n你是谁？");
                    count++;
                    break;
                case 10:
                    woodAndFlowerSpeechBubble.showDialog("这是我在这儿认识的新朋友，雪王。");
                    count++;
                    break;
                case 11:
                    peaSpeechBubble.showDialog("你们是.......朋友？");
                    count++;
                    break;
                case 12:
                    zombieIntroduce1.SetActive(true);
                    pea.GetComponent<Plant>().die("");
                    wood.GetComponent<Plant>().die("");
                    snowKing.GetComponent<Plant>().die("");
                    count++;
                    break;
                default:
                    break;
            }
        }
    }

    private void flowerLastWords()
    {
        woodAndFlowerSpeechBubble.showDialog("豌豆......下士......我......不行了......你......一定要......");
        count = 2;
    }

    private void peaShock()
    {
        peaSpeechBubble.showDialog("向日葵指挥官！");
        count = 3;
    }

    private void woodPrepare()
    {
        woodAndFlowerSpeechBubble.showDialog("不好，那些东西又来了！大家小心！");
        count = 8;
    }

    private void snowKingAppear()
    {
        kingSpeechBubble.showDialog("不要怕，本王来祝你们一臂之力。");
        snowKing = GameObject.Find("Plant-0-2")
            .GetComponent<PlantGrid>().plantByGod("SnowKing");
        count = 9;
    }

    public void clickNext1()
    {
        zombieIntroduce1.SetActive(false);
        zombieIntroduce2.SetActive(true);
    }

    public void clickNext2()
    {
        zombieIntroduce2.SetActive(false);
        plantIntroduce.SetActive(true);
    }

    public void clickStart()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/UI/plantPanelAppear"),
                new Vector3(0, 0, -10)
            );
        plantIntroduce.SetActive(false);

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }

}
