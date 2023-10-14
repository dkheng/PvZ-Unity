using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagement : MonoBehaviour
{
    //僵尸相关
    public GameObject[] zombies;   //可生成僵尸列表
    Dictionary<string, int> zombiesName = new Dictionary<string, int>();   //僵尸名称与对象索引的字典

    //僵尸数量相关
    int zombieNum_now = 0;   //全场现有僵尸数量，用于确保场上僵尸清零后再产生一波

    //僵尸生成相关
    List<int> rowList = new List<int>();  //可生成僵尸行列表
                                          //在某行上生成后就把该行剔除，以避免僵尸一直出现在某一行
    public string generateFunc = "generateZombies";  //僵尸生成函数的名称，便于调用特殊关卡的生成函数

    //僵尸初始位置
    float initPos_x = 6.0f;

    //时间轴相关
    TimeNodes timeNodes;   //从json读取出来的时间节点列表
    int nowNode_index = 0;   //当前是第几个时间节点
    int nodeCount = 0;  //总共有几个时间节点
    TimeNode nowNode;   //当前时间节点
    bool waitWave = false;   //是否正在等待一波僵尸的生成
    bool isOver = false;   //关卡是否结束
    DecreasingSlider flagMeter;   //关卡进度条组件

    //字幕相关
    public Caption caption;  //字幕组件

    AudioSource audioSource;   //自身AudioSource组件

    private void Awake()
    {
        //获取对象与组件
        audioSource = GetComponent<AudioSource>();
        flagMeter = GameObject.Find("FlagMeter-Slider").GetComponent<DecreasingSlider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //读取关卡json文件并转换为变量对象
        string info = Resources.Load<TextAsset>(
            "Json/ZombieData/Level" + GameManagement.levelData.level
        ).text;
        timeNodes = JsonUtility.FromJson<TimeNodes>(info);

        //初始化可生成的僵尸的名称列表
        for(int i = 0; i < zombies.Length; i++)
        {
            zombiesName.Add(zombies[i].name, i);
        }

        //初始化僵尸生成相关
        initRowList();

        //初始化时间轴相关
        nodeCount = timeNodes.info.Count;
        nowNode_index = 0;
        nowNode = timeNodes.info[nowNode_index];
    }

    public void activate()
    {
        //准备第一波
        Invoke("enterTimeNode", nowNode.deltaTime);
    }

    private void enterTimeNode()
    {
        if (nowNode.isWave == false)   //不是一波
        {
            Invoke(generateFunc, 0);
        }
        else   //是一波
        {
            if (zombieNum_now == 0)   //场上僵尸清零后才产生一波
            {
                waitWave = false;
                if(nowNode.isFinalWave == false) caption.showWave();
                else caption.showFinalWave();
                Invoke(generateFunc, 0);
            }
            else waitWave = true;
        }
    }

    //逐个生成当前时间节点的所有僵尸
    //此函数并非未引用过，而是以Invoke方式调用未识别
    private void generateZombies()
    {
        for (int i = 0; i < nowNode.number; i++)
        {
            //获取随机行
            int randY = rowList[Random.Range(0, rowList.Count)];
            rowList.Remove(randY);
            if (rowList.Count == 0) initRowList();
            //生成僵尸
            GameObject newZombie = Instantiate(zombies[zombiesName[nowNode.zombie]],
                new Vector3(initPos_x, GameManagement.levelData.zombieInitPosY[randY], 0),
                Quaternion.Euler(0, 0, 0),
                transform);
            newZombie.GetComponent<Zombie>().setPosRow(randY);
            addZombieNumAll();
        }
        changeTimeNode();
    }

    //切换时间节点到下一个并执行相应修改
    private void changeTimeNode()
    {
        //如果刚生成的是第一批僵尸
        if(nowNode_index == 0)
        {
            audioSource.Play(); //播放警报音效
            InvokeRepeating("groan", 0, 5);   //每隔5秒执行僵尸叹息音效函数
        }

        //切换时间节点
        nowNode_index++;
        if (nowNode_index < nodeCount)   //后面还有时间节点，就切换
        {
            nowNode = timeNodes.info[nowNode_index];
            Invoke("enterTimeNode", nowNode.deltaTime);
        }
        else   //没了就是游戏结束
        {
            isOver = true;
        }

        //更新关卡进度条
        flagMeter.setValue((nodeCount - nowNode_index) / (float)nodeCount);
    }

    //创造僵尸，上帝模式，用于对话
    public void createZombieByGod(string name, int posRow)
    {
        GameObject newZombie = Instantiate(zombies[zombiesName[name]],
            new Vector3(initPos_x, GameManagement.levelData.zombieInitPosY[posRow], 0),
            Quaternion.Euler(0, 0, 0),
            transform);
        newZombie.GetComponent<Zombie>().setPosRow(posRow);
        newZombie.GetComponent<Zombie>().cancelSleep();
        addZombieNumAll();
    }

    //初始化可生成僵尸行列表
    private void initRowList()
    {
        for (int i = 0; i < GameManagement.levelData.landRowCount; i++)
        {
            rowList.Add(i);
        }
    }

    private void groan()
    {
        int rand = Random.Range(1, 50);
        if(rand <= 6)
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Zombies/groan" + rand));
        }
    }

    public void addZombieNumAll()
    {
        zombieNum_now++;
    }

    //场上僵尸数量减一
    public void minusZombieNumAll()
    {
        zombieNum_now--;  //场上僵尸数量减一

        //若场上已无僵尸
        if (zombieNum_now <= 0)
        {
            //若一大波僵尸正等待生成，则生成
            if(waitWave == true)
            {
                enterTimeNode();
            }
            //若所有僵尸已生成，则游戏结束
            else if(isOver == true)
            {
                GameObject.Find("Game Management").GetComponent<GameManagement>().win();
            }
        }
    }

    #region 特殊关卡专用函数区域

    //专用于第二关的僵尸生成函数
    //此函数并非未引用过，而是以Invoke方式调用未识别
    private void generateZombies_level2()
    {
        //此关卡中nowNode.number代表有几队僵尸
        for (int i = 0; i < nowNode.number; i++)
        {
            int zombieNum = Random.Range(3, 6);  //随机生成本队僵尸数量
            ChineseZombie last = null;  //上一个僵尸

            //获取随机行
            int randY = rowList[Random.Range(0, rowList.Count)];
            rowList.Remove(randY);
            if (rowList.Count == 0) initRowList();

            //该队僵尸随机横坐标偏移量
            float allOffset = Random.Range(0.0f, 2.0f);

            float offset = 0.85f;  //各个僵尸之间横坐标偏移量

            for (int j = 0; j < zombieNum; j++)
            {
                //生成僵尸
                GameObject newZombie = Instantiate(zombies[zombiesName[nowNode.zombie]],
                    new Vector3(
                        initPos_x + allOffset + j * offset,
                        GameManagement.levelData.zombieInitPosY[randY],
                        0
                    ),
                    Quaternion.Euler(0, 0, 0),
                    transform);
                newZombie.GetComponent<Zombie>().setPosRow(randY);
                addZombieNumAll();
                //设置僵尸链表信息
                if(last == null)
                {
                    last = newZombie.GetComponent<ChineseZombie>();
                    last.prior = null;
                    last.isCaptain = true;
                }
                else
                {
                    ChineseZombie newCZ = newZombie.GetComponent<ChineseZombie>();
                    last.next = newCZ;
                    newCZ.prior = last;
                    last = newCZ;
                }
            }
            last.next = null;
        }

        changeTimeNode();
    }

    //用于随机创造幽灵
    public void createGhost()
    {
        //获取随机行
        int randY = Random.Range(0, GameManagement.levelData.rowCount);
        //生成幽灵
        GameObject newZombie = Instantiate(zombies[zombiesName["Ghost"]],
            new Vector3(initPos_x, GameManagement.levelData.zombieInitPosY[randY], 0),
            Quaternion.Euler(0, 0, 0),
            transform);
        newZombie.GetComponent<Zombie>().setPosRow(randY);
        //随机时间后再创造
        if(nowNode_index <= 8)
            Invoke("createGhost", Random.Range(15.0f, 20.0f));
        else Invoke("createGhost", Random.Range(5.0f, 10.0f));
    }

    #endregion
}

[System.Serializable]
public class TimeNode
{
    public float deltaTime;  //多长时间后开始下一波进攻
    public bool isWave;  //是否为一波
    public bool isFinalWave;   //是否为最后一波
    public int number;   //僵尸数量
    public string zombie;   //僵尸名称
}

[System.Serializable]
public class TimeNodes
{
    public List<TimeNode> info;
}