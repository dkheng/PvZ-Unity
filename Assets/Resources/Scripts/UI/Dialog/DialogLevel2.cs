using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevel2 : MonoBehaviour
{
    public SpeechBubble flowerSpeechBubble;   //���տ��Ի���
    public SpeechBubble nutSpeechBubble;    //���ǽ�Ի���
    public SpeechBubble peaSpeechBubble;   //�㶹�Ի���
    public GameObject introduce;   //��ʬ������

    GameObject flower;
    GameObject nut;
    GameObject pea;

    BGMusicControl bGMusicControl;   //�����������Դ���

    int count = 0;  //�Ի���������ǰ�ǵڼ����Ի�

    private void Awake()
    {
        //��ȡ���
        bGMusicControl = GameObject.Find("Background").GetComponent<BGMusicControl>();
        //��ֲ����Ի���ֲ��
        flower = GameObject.Find("Plant-0-2")
            .GetComponent<PlantGrid>().plantByGod("SunFlowerForDialog");
        nut = GameObject.Find("Plant-4-2")
            .GetComponent<PlantGrid>().plantByGod("WallNut");
    }

    // Start is called before the first frame update
    void Start()
    {
        //���ñ�������
        bGMusicControl.changeMusic("Music_Night");

        flowerSpeechBubble.showDialog("�����ҵ����ˣ������ʿ��");
    }

    // Update is called once per frame
    void Update()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 0:
                    nutSpeechBubble.showDialog("���δ����ָ�ӹٱ���������");
                    count++;
                    break;
                case 1:
                    flowerSpeechBubble.showDialog("���������������שͷ�ϴ����治���");
                    count++;
                    break;
                case 2:
                    flowerSpeechBubble.showDialog("���������ʿ�ھͺ���");
                    count++;
                    break;
                case 3:
                    nutSpeechBubble.showDialog("����֮");
                    count++;
                    break;
                case 4:
                    peaSpeechBubble.showDialog("�����ˣ������ˣ�");
                    count++;
                    break;
                case 5:
                    flowerSpeechBubble.showDialog("��ʲô����û���Ҹ������ʿ�������");
                    count++;
                    break;
                case 6:
                    pea = GameObject.Find("Plant-8-2")
                        .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
                    peaSpeechBubble.gameObject.GetComponent<RectTransform>().localPosition -=
                        new Vector3(85, 0, 0);
                    peaSpeechBubble.showDialog("�����ˣ�����ö���ֵĽ�ʬ����Ҫ�������");
                    count++;
                    break;
                case 7:
                    flowerSpeechBubble.showDialog("ʲô��������ϵģ����⹽ָ�ӹ��ֲ��ڣ���������");
                    count++;
                    break;
                case 8:
                    nutSpeechBubble.showDialog("ָ�ӹ������ǳ�ڴ�����֮����ҹ����ǧ�����");
                    count++;
                    break;
                case 9:
                    peaSpeechBubble.showDialog("ʲôǧ�����ֵܣ�����ô��������޵�");
                    count++;
                    break;
                case 10:
                    flowerSpeechBubble.showDialog("�����ˣ���Ҹ��͸�λ�������տ�ָ�ӹٿɲ����˵�����");
                    count++;
                    break;
                case 11:
                    introduce.SetActive(true);

                    //�Ի�ֲ����ʧ
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

        //�л���������
        bGMusicControl.changeMusicSmoothly("Music_Night_Wall");

        GameObject.Find("Game Management").GetComponent<GameManagement>().awakeAll();
        gameObject.SetActive(false);
    }
}
