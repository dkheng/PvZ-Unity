using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //冷却贴图
    public GameObject upperImageObj;
    public Image lowerImage;
    public GameObject lowerImageObj;

    public Button myButton;   //自身Button组件

    //冷却时间与冷却状态
    public float coolingTime;
    float timer;
    bool coolingState = true;

    //阳光是否充足状态
    bool sunEnough;

    //种植相关
    PlantingManagement planting;
    public string plantName;
    public int sunNeeded;

    // Start is called before the first frame update
    void Start()
    {
        //该组件须由管理对象加载，故在Start获取
        planting = GameObject.Find("Planting Management").GetComponent<PlantingManagement>();

        if (coolingTime > 10f) cooling();
        else endCooling();
    }

    // Update is called once per frame
    void Update()
    {
        if(coolingState == true)
        {
            timer += Time.deltaTime;
            if (timer / coolingTime < 1)
                lowerImage.rectTransform.localScale = new Vector3(1, 1 - timer / coolingTime, 1);
            else endCooling();
        }
    }

    public void cooling()
    {
        coolingState = true;
        timer = 0;
        lowerImage.fillAmount = 1;
        upperImageObj.SetActive(true);
        lowerImageObj.SetActive(true);
        myButton.enabled = false;
    }

    private void endCooling()
    {
        coolingState = false;
        lowerImageObj.SetActive(false);
        if (sunEnough)
        {
            upperImageObj.SetActive(false);
            myButton.enabled = true;
        }
    }

    public void updateSunEnough(bool state)
    {
        if(state == true)
        {
            sunEnough = true;
            if (coolingState == false)
            {
                upperImageObj.SetActive(false);
                myButton.enabled = true;
            }
        }
        else
        {
            sunEnough = false;
            upperImageObj.SetActive(true);
            myButton.enabled = false;
        }
    }    

    public void click()
    {
        //播放音效
        gameObject.GetComponent<AudioSource>().Play();

        //转给种植管理
        planting.clickPlant(plantName, gameObject.GetComponent<Card>());
    }
}
