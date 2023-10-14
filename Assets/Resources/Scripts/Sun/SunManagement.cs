using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManagement : MonoBehaviour
{
    //天空太阳预制体
    public GameObject skysunPrefab;
    public GameObject fullMoonPrefab;
    public GameObject crescentMoonPrefab;

    string createFunc;   //创造阳光函数名，分为白天和夜晚

    //太阳掉落计时
    float minInterval = 10f, maxInterval = 20f;
    //太阳初始位置
    float posY =  3.4f;
    //太阳掉落位置x轴限制
    const float leftEdge = -4.4f;
    const float rightEdge = 2.8f;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManagement.levelData.isDay)
            createFunc = "createSun";
        else createFunc = "createMoon";

        Invoke(createFunc, Random.Range(minInterval, maxInterval));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) clickSun();
    }

    private void createSun()
    {
        Instantiate(
            skysunPrefab, 
            new Vector3(Random.Range(leftEdge, rightEdge), posY, 0), 
            Quaternion.Euler(0, 0, 0), 
            transform
        );

        Invoke(createFunc, Random.Range(minInterval, maxInterval));
    }

    private void createMoon()
    {
        GameObject randPrefab;
        if (Random.Range(0.0f, 10.0f) > 3.0f) randPrefab = fullMoonPrefab;
        else randPrefab = crescentMoonPrefab;

        Instantiate(
            randPrefab,
            new Vector3(Random.Range(leftEdge, rightEdge), posY, 0),
            Quaternion.Euler(0, 0, 0),
            transform
        );

        Invoke(createFunc, Random.Range(minInterval, maxInterval));
    }

    private void clickSun()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] allSun = Physics2D.OverlapPointAll(mouseWorldPos, LayerMask.GetMask("Sun"));
        if (allSun.Length > 0)
        {
            allSun[allSun.Length - 1].gameObject.GetComponent<SunBase>().bePickedUp();
        }
    }
}
