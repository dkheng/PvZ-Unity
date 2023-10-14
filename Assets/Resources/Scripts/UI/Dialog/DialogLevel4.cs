using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevel4 : MonoBehaviour
{
    public SpeechBubble peaSpeechBubble;   //�㶹�Ի���
    public SpeechBubble woodAndFlowerSpeechBubble;   //�������տ��Ի���
    public SpeechBubble kingSpeechBubble;   //ѩ���Ի���
    public GameObject zombieIntroduce1;   //��ʬ������1
    public GameObject zombieIntroduce2;   //��ʬ������2
    public GameObject plantIntroduce;     //ֲ�������

    GameObject flower;
    GameObject pea;
    GameObject wood;
    GameObject snowKing;

    int count = 0;  //�Ի���������ǰ�ǵڼ����Ի�
    private void Awake()
    {
        //��ֲ����Ի���ֲ��
        flower = GameObject.Find("Plant-0-3")
            .GetComponent<PlantGrid>().plantByGod("SunFlowerForDialog");
        pea = GameObject.Find("Plant-1-3")
            .GetComponent<PlantGrid>().plantByGod("PeaShooterSingle");
    }

    // Start is called before the first frame update
    void Start()
    {
        woodAndFlowerSpeechBubble.showDialog("��......���磡");
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
                    peaSpeechBubble.showDialog("���տ�ָ�ӹ٣��㻹����......���磡");
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
                    woodAndFlowerSpeechBubble.showDialog("���õ���");
                    count++;
                    break;
                case 4:
                    wood = GameObject.Find("Plant-0-3")
                        .GetComponent<PlantGrid>().plantByGod("TorchWood");
                    woodAndFlowerSpeechBubble.gameObject.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Sprites/UI/SpeechBubble/SpeechBubble2");
                    woodAndFlowerSpeechBubble.transform.localPosition += new Vector3(0, 71, 0);
                    woodAndFlowerSpeechBubble.showDialog("���Ѿ���ָ�ӹ�ȥɽ������Ϣ��");
                    count++;
                    break;
                case 5:
                    peaSpeechBubble.showDialog("�����ξ��������̫���ˣ�");
                    count++;
                    break;
                case 6:
                    peaSpeechBubble.showDialog("�������֣�");
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
                    peaSpeechBubble.showDialog("�ţ�\n����˭��");
                    count++;
                    break;
                case 10:
                    woodAndFlowerSpeechBubble.showDialog("�������������ʶ�������ѣ�ѩ����");
                    count++;
                    break;
                case 11:
                    peaSpeechBubble.showDialog("������.......���ѣ�");
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
        woodAndFlowerSpeechBubble.showDialog("�㶹......��ʿ......��......������......��......һ��Ҫ......");
        count = 2;
    }

    private void peaShock()
    {
        peaSpeechBubble.showDialog("���տ�ָ�ӹ٣�");
        count = 3;
    }

    private void woodPrepare()
    {
        woodAndFlowerSpeechBubble.showDialog("���ã���Щ���������ˣ����С�ģ�");
        count = 8;
    }

    private void snowKingAppear()
    {
        kingSpeechBubble.showDialog("��Ҫ�£�������ף����һ��֮����");
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
