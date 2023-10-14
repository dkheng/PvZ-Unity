using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevel1 : MonoBehaviour
{
    public CrazyDave crazyDave;   //疯狂戴夫的脚本组件
    public SpeechBubble peaSpeechBubble;    //豌豆对话框
    public SpeechBubble daveSpeechBubble;   //疯狂戴夫对话框

    SpriteRenderer pea_spriteRenderer;   //生成的豌豆射手的SpriteRenderer，用于翻转它

    int count = 0;  //对话计数，当前是第几条对话

    private void Awake()
    {
        pea_spriteRenderer = GameObject.Find("Plant-4-2")
            .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle")
            .GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("flipPea", 2f);
        Invoke("showFirstDialog", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch(count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("哦~我的除草机坏了");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("You know......我不能没有它");
                    count++;
                    break;
                case 3:
                    peaSpeechBubble.showDialog("听我说，谢谢你！");
                    count++;
                    break;
                case 4:
                    crazyDave.smallTalk("不客气！");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    flipPea();
                    Invoke("showPeaDialog", 2f);
                    count = -1;   //这样执行下面递增语句后count为0
                                  //此时点击不触发事件，Invoke执行后才进入下一事件
                    break;
                case 6:
                    GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    private void flipPea()
    {
        pea_spriteRenderer.flipX = !(pea_spriteRenderer.flipX);
    }

    private void showFirstDialog()
    {
        peaSpeechBubble.showDialog("Where is my 小推车！"); //展示对话
        count++;   //计数递增
    }

    //这是戴夫离开后豌豆的第一句话
    private void showPeaDialog()
    {
        peaSpeechBubble.showDialog("哼~就这些货色，根本用不着小推车"); //展示对话

        count = 6;   //计数递增
    }
}
