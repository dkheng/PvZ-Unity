using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevel2 : MonoBehaviour
{
    public SpeechBubble flowerSpeechBubble;   //向日葵对话框
    public SpeechBubble nutSpeechBubble;    //坚果墙对话框
    public SpeechBubble peaSpeechBubble;   //豌豆对话框
    public GameObject introduce;   //僵尸介绍栏

    GameObject flower;
    GameObject nut;
    GameObject pea;

    BGMusicControl bGMusicControl;   //背景对象的音源组件

    int count = 0;  //对话计数，当前是第几条对话

    private void Awake()
    {
        //获取组件
        bGMusicControl = GameObject.Find("Background").GetComponent<BGMusicControl>();
        //种植参与对话的植物
        flower = GameObject.Find("Plant-0-2")
            .GetComponent<PlantGrid>().plantByGod("SunFlowerForDialog");
        nut = GameObject.Find("Plant-4-2")
            .GetComponent<PlantGrid>().plantByGod("WallNut");
    }

    // Start is called before the first frame update
    void Start()
    {
        //设置背景音乐
        bGMusicControl.changeMusic("Music_Night");

        flowerSpeechBubble.showDialog("终于找到你了，坚果下士！");
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
                    nutSpeechBubble.showDialog("许久未见，指挥官别来无恙啊？");
                    count++;
                    break;
                case 1:
                    flowerSpeechBubble.showDialog("还好啦，就是这大砖头上呆着真不舒服");
                    count++;
                    break;
                case 2:
                    flowerSpeechBubble.showDialog("如果花盆下士在就好了");
                    count++;
                    break;
                case 3:
                    nutSpeechBubble.showDialog("适则安之");
                    count++;
                    break;
                case 4:
                    peaSpeechBubble.showDialog("不好了！不好了！");
                    count++;
                    break;
                case 5:
                    flowerSpeechBubble.showDialog("吵什么吵，没看我跟坚果下士正叙旧呢");
                    count++;
                    break;
                case 6:
                    pea = GameObject.Find("Plant-8-2")
                        .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
                    peaSpeechBubble.gameObject.GetComponent<RectTransform>().localPosition -=
                        new Vector3(85, 0, 0);
                    peaSpeechBubble.showDialog("别聊了，外面好多奇怪的僵尸，就要冲进来了");
                    count++;
                    break;
                case 7:
                    flowerSpeechBubble.showDialog("什么！这大晚上的，阳光菇指挥官又不在，这下完了");
                    count++;
                    break;
                case 8:
                    nutSpeechBubble.showDialog("指挥官无须忧愁，在此奇妙之世，夜亦有千里皓月");
                    count++;
                    break;
                case 9:
                    peaSpeechBubble.showDialog("什么千里？坚果兄弟，你怎么变得文邹邹的");
                    count++;
                    break;
                case 10:
                    flowerSpeechBubble.showDialog("不管了，大家各就各位，我向日葵指挥官可不是浪得虚名");
                    count++;
                    break;
                case 11:
                    introduce.SetActive(true);

                    //对话植物消失
                    flower.GetComponent<Plant>().die("");
                    nut.GetComponent<Plant>().die("");
                    pea.GetComponent<Plant>().die("");

                    count++;
                    break;
                default:
                    break;
            }
        }
    }

    public void clickStart()
    {
        AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/UI/graveButtonClick"),
                new Vector3(0, 0, -10)
            );
        introduce.SetActive(false);

        //切换背景音乐
        bGMusicControl.changeMusicSmoothly("Music_Night_Wall");

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }
}
