using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevel1 : MonoBehaviour
{
    public CrazyDave crazyDave;   //������Ľű����
    public SpeechBubble peaSpeechBubble;    //�㶹�Ի���
    public SpeechBubble daveSpeechBubble;   //������Ի���

    SpriteRenderer pea_spriteRenderer;   //���ɵ��㶹���ֵ�SpriteRenderer�����ڷ�ת��

    int count = 0;  //�Ի���������ǰ�ǵڼ����Ի�

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
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch(count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("Ŷ~�ҵĳ��ݻ�����");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("You know......�Ҳ���û����");
                    count++;
                    break;
                case 3:
                    peaSpeechBubble.showDialog("����˵��лл�㣡");
                    count++;
                    break;
                case 4:
                    crazyDave.smallTalk("��������");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    flipPea();
                    Invoke("showPeaDialog", 2f);
                    count = -1;   //����ִ�������������countΪ0
                                  //��ʱ����������¼���Invokeִ�к�Ž�����һ�¼�
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
        peaSpeechBubble.showDialog("Where is my С�Ƴ���"); //չʾ�Ի�
        count++;   //��������
    }

    //���Ǵ����뿪���㶹�ĵ�һ�仰
    private void showPeaDialog()
    {
        peaSpeechBubble.showDialog("��~����Щ��ɫ�������ò���С�Ƴ�"); //չʾ�Ի�

        count = 6;   //��������
    }
}
