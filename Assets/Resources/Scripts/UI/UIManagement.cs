using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public GameObject topMotionPanel;
    public GameObject bottomMotionPanel;
    public GameObject seedBank;
    public GameObject shovelBank;
    public Text levelNameText;

    public GameObject cardGroup;   //卡槽群组

    // Start is called before the first frame update
    public void initUI()
    {
        //加载关卡名字
        levelNameText.text = GameManagement.levelData.levelName;

        //加载卡槽群组，并设置相关UI的大小位置
        List<string> plantCards = GameManagement.levelData.plantCards;
        List<Card> cards = new List<Card>();
        foreach (string plant in plantCards)
        {
            cards.Add((
                    Instantiate(
                        Resources.Load<Object>("Prefabs/UI/Card/" + plant + "Card"),
                        cardGroup.transform
                    ) as GameObject
                ).GetComponent<Card>());
        }
        GameObject.Find("Sun Text").GetComponent<SunNumber>().setCardGroup(cards);
        float cardGroupWidth = plantCards.Count * 43 - 1;
        cardGroup.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth);
        seedBank.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardGroupWidth + 78);
        shovelBank.GetComponent<RectTransform>()
            .SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, cardGroupWidth + 108, 60);
    }

    public void appear()
    {
        //卡槽群组本为不活跃，以避免剧情期间卡槽冷却减少
        cardGroup.SetActive(true);

        topMotionPanel.GetComponent<MotionPanel>().startMove();
        bottomMotionPanel.GetComponent<MotionPanel>().startMove();
    }
}
